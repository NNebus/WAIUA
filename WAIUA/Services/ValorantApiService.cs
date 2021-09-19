using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WAIUA.Api;
using WAIUA.Models;

namespace WAIUA.Services
{
    public class ValorantApiService
    {
        private string Port { get; set; }
        private string LPassword { get; set; }
        private string Protocol { get; set; }
        private string GameVersion {  get; set; }

        private static readonly string ClientPlatform = "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9";
        private Account Account { get; set; }

        public ValorantApiService(Account account) {
            Account = account;
            account.UniqueId = GetUniqueAccountId();
        }
        public Player GetPlayer() {
            string Region = GetLocalRegion().Split('_')[0];
            string url = ApiUrl.RiotPlayerServiceUrl.Replace("{###Region###}", Region);


            string[] body = new string[1] {
                Account.UniqueId
            };

            RestClient client = new(url) {
                CookieContainer = Account.CookieContainer
            };

            RestRequest request = new(Method.PUT) {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("X-Riot-Entitlements-JWT", $"{Account.EntitlementToken}")
            .AddHeader("Authorization", $"Bearer {Account.AccessToken}")
            .AddJsonBody(body);

            var response = client.Put(request);
            string content = client.Execute(request).Content;

            content = content.Replace("[", "");
            content = content.Replace("]", "");

            var uinfo = JsonConvert.DeserializeObject(content);
            JToken uinfoObj = JObject.FromObject(uinfo);

            Player player = new() {
                Name = uinfoObj["GameName"].Value<string>(),
                Tag = uinfoObj["TagLine"].Value<string>(),
                Region = Region
            };

            return player;
        }


        public string GetUniqueAccountId()
        {
            if (string.IsNullOrEmpty(Account.AccessToken)) {
                Account.GetTokens();
            }

            RestClient client = new(new Uri(ApiUrl.RiotUserInfoServiceUrl));
            RestRequest request = new(Method.POST);
            request.AddHeader("Authorization", $"Bearer {Account.AccessToken}");
            request.AddJsonBody("{}");

            string response = client.Execute(request).Content;
            var PlayerInfo = JsonConvert.DeserializeObject(response);
            JToken PUUIDObj = JObject.FromObject(PlayerInfo);
            return PUUIDObj["sub"].Value<string>();
        }


        public bool GetConnectionData()
        {
            var lockfileLocation = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Riot Games\Riot Client\Config\lockfile";

            if (File.Exists(lockfileLocation))
            {
                using (FileStream fileStream = new(lockfileLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamReader sr = new(fileStream))
                {
                    string[] parts = sr.ReadToEnd().Split(":");
                    Port = parts[2];
                    LPassword = parts[3];
                    Protocol = parts[4];
                }
                return true;
            }
            else
            {
                throw new ValorantNotRunningException("Could not login");
                return false; // not logged in
            }
        }

        public string GetLatestGameVersion()
        {
            RestClient client = new(new Uri(ApiUrl.ValorantApi));
            RestRequest request = new(Method.GET);
            var response = client.Get(request);
            string content = response.Content;
            var deserializedContent = JsonConvert.DeserializeObject(content);
            JToken responseObj = JObject.FromObject(deserializedContent);

            return responseObj["data"]["riotClientVersion"].Value<string>();
        }

        public string GetLocalRegion() {
            if (String.IsNullOrEmpty(LPassword)) {
                GetConnectionData();
            }

            if (String.IsNullOrEmpty(GameVersion)) {
                GameVersion = GetLatestGameVersion();
            }

            String Region = "";
            String Shard = "";

            // todo: refactor
            RestClient client = new RestClient(new Uri($"https://127.0.0.1:{Port}/product-session/v1/external-sessions")) {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            RestRequest request = new(Method.GET) { 
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"riot:{LPassword}"))}")
            .AddHeader("X-Riot-ClientPlatform", ClientPlatform)
            .AddHeader("X-Riot-ClientVersion", GameVersion);

            var response = client.Get(request);
            string content = client.Execute(request).Content;
            JObject root = JObject.Parse(content);
            JProperty property = (JProperty)root.First;
            var fullstring = (property.Value["launchConfiguration"]["arguments"][3]);
            string[] parts = fullstring.ToString().Split(new char[] { '=', '&' });
            string output = parts[1];
            if (output == "latam")
            {
                Region = "na";
                Shard = "latam";
            }
            else if (output == "br")
            {
                Region = "na";
                Shard = "br";
            }
            else
            {
                Region = output;
                Shard = output;
            }

            return $"{Region}_{Shard}";
        }

        public string GetCurrentMatchId() {
            Match match = new();

            if (string.IsNullOrEmpty(Account.AccessToken) || string.IsNullOrEmpty(Account.EntitlementToken))
            {
                Account.GetTokens();
            }

            if (string.IsNullOrEmpty(Account.Region)) {
                Account.Region = GetLocalRegion();
            }

            if(string.IsNullOrEmpty(Account.UniqueId)) {
                Account.UniqueId = GetUniqueAccountId();
            }

            if (string.IsNullOrEmpty(Account.AccessToken) || string.IsNullOrEmpty(Account.EntitlementToken)) {
                Account.GetTokens();
            }

            try
            {
                // todo: refactor
                string url = ApiUrl.RiotMatchServiceUrl
                    .Replace("{###Shard###}", Account.Region.Split("_")[1])
                    .Replace("{###Region###}", Account.Region.Split("_")[0])
                    .Replace("{###PUUID###}", Account.UniqueId);

                RestClient client = new(url) {
                    CookieContainer = Account.CookieContainer
                };
                RestRequest request = new(Method.GET);
                request.AddHeader("X-Riot-Entitlements-JWT", Account.EntitlementToken)
                .AddHeader("Authorization", $"Bearer {Account.AccessToken}");
                string response = client.Execute(request).Content;
                var matchinfo = JsonConvert.DeserializeObject(response);
                JToken matchinfoObj = JObject.FromObject(matchinfo);
                match.Id = matchinfoObj["MatchID"].Value<string>();
            }
            catch (Exception e)
            {
                // Todo: Add Exception for non started matches
                System.Diagnostics.Debug.WriteLine(e);
            }

            return match.Id;
        }

        public dynamic GetMatchData(string matchId)
        {
            Match match = new();

            if (string.IsNullOrEmpty(Account.AccessToken) || string.IsNullOrEmpty(Account.EntitlementToken))
            {
                Account.GetTokens();
            }

            if (string.IsNullOrEmpty(Account.Region))
            {
                Account.Region = GetLocalRegion();
            }

            if (string.IsNullOrEmpty(Account.UniqueId))
            {
                Account.UniqueId = GetUniqueAccountId();
            }

            if (string.IsNullOrEmpty(Account.AccessToken) || string.IsNullOrEmpty(Account.EntitlementToken))
            {
                Account.GetTokens();
            }

            try
            {
                // todo: refactor
                string url = ApiUrl.RiotMatchServiceUrl2
                    .Replace("{###Shard###}", Account.Region.Split("_")[1])
                    .Replace("{###Region###}", Account.Region.Split("_")[0])
                    .Replace("{###MatchId###}", matchId);

                RestClient client = new(url)
                {
                    CookieContainer = Account.CookieContainer
                };
                RestRequest request = new(Method.GET);
                request.AddHeader("X-Riot-Entitlements-JWT", Account.EntitlementToken)
                .AddHeader("Authorization", $"Bearer {Account.AccessToken}");

                string response = client.Execute(request).Content;


                JObject matchInfo = JsonConvert.DeserializeObject(response) as JObject;
                JArray players = (JArray)matchInfo["Players"];

                List<Player> matchPlayers = players.Select(p => new Player() { 
                    User = new User() { 
                        Id = (string)p["Subject"]
                    },
                    Team = ((string)p["TeamID"]) == "Blue" ? Team.Blue : Team.Red,
                    CardId = (string)p["PlayerIdentity"]["PlayerCardID"],
                    TitleId = (string)p["PlayerIdentity"]["PlayerTitleID"],
                    AccountLevel = (int)p["PlayerIdentity"]["AccountLevel"],
                    Incognito = (bool)p["PlayerIdentity"]["Incognito"],
                    HideAccountLevel = (bool)p["PlayerIdentity"]["HideAccountLevel"],
                    Agent = AgentHelper.GetFromCharacterId((string)p["PlayerIdentity"]["CharacterID"]),
                }).ToList();


                JToken matchinfoObj = JObject.FromObject(matchInfo);
                match.Id = matchinfoObj["MatchID"].Value<string>();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw new Exception(e.Message);
            }

            return match;
        }
    }
}
