using System.Security;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WAIUA.Api;

namespace WAIUA.Models
{
    public class Game {
        public string Season {get; set;}
        public string GameVersion => GetGameVersion();
        public string LatestGameVersion => GetLatestGameVersion();


        private string GetGameVersion () {
            return GetLatestGameVersion(); // TMP
        }

        private string GetLatestGameVersion () {
            RestClient client = new RestClient(new Uri(ApiUrl.ValorantApi));
            RestRequest request = new RestRequest(Method.GET);
            var response = client.Get(request);
            string content = response.Content;
            var deserializedContent = JsonConvert.DeserializeObject(content);
            JToken responseObj = JObject.FromObject(deserializedContent);

            return responseObj["data"]["riotClientVersion"].Value<string>();
        }
    }
}