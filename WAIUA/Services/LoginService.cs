using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Dynamic;
using System.Net;
using System.Text.RegularExpressions;
using WAIUA.Api;
using WAIUA.Models;

namespace WAIUA.Services
{
    public class LoginService
    {
        public CookieContainer CookieContainer {  get; set; }
        private Account Account {  get; set; }



        public LoginService(CookieContainer cookieContainer, Account account) {
            CookieContainer = cookieContainer;
            Account = account;
        }


        public void GetAuthorization()
        {
            new RestClient(ApiUrl.GetRiotAuthorizationServiceUrl()) {
                CookieContainer = CookieContainer
            }.Execute(new RestRequest(Method.POST).AddJsonBody(GetAuthorizationJson()));
        }

        public RestClient GetAuthorizedRestClient() {
            RestClient restClient = new(ApiUrl.GetRiotAuthorizationServiceUrl())
            {
                CookieContainer = CookieContainer
            };
            
            restClient.Execute(new RestRequest(Method.POST).AddJsonBody(GetAuthorizationJson()));

            return restClient;
        }

        private string GetAuthorizationJson()
        {
            dynamic authConfig = new ExpandoObject();
            authConfig.client_id = "play-valorant-web-prod";
            authConfig.nonce = "1";
            authConfig.redirect_uri = ApiUrl.GetValorantOptInApiUrl();
            authConfig.response_type = "token id_token";
            authConfig.scope = "account openid";

            return JsonConvert.SerializeObject(authConfig);
        }

        private string GetAuthenticationJson()
        {
            dynamic authConfig = new ExpandoObject();
            authConfig.type = "auth";
            authConfig.username = Account.Username;
            authConfig.password = Account.Password;
            authConfig.remember = "true";
            authConfig.language = "en_US"; // todo: get actual language

            return JsonConvert.SerializeObject(authConfig);
        }

        public string GetAuthentication()
        {
            GetAuthorization();

            return new RestClient(ApiUrl.GetRiotAuthorizationServiceUrl())
            {
                CookieContainer = CookieContainer
            }.Execute(new RestRequest(Method.PUT).AddJsonBody(GetAuthenticationJson())).Content;
        }

        public string GetJwt() {

            var deserializedAuthentication = JsonConvert.DeserializeObject(GetAuthentication());
            JToken authenticationObject = JObject.FromObject(deserializedAuthentication);

            string authURL = authenticationObject["response"]["parameters"]["uri"].Value<string>();
            string bearerToken = Regex.Match(authURL, @"access_token=(.+?)&scope=").Groups[1].Value;
            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new Exception($"JWT is null or empty");
            }

            return bearerToken;
        }

        public string GetEntitlementToken() {

            RestClient client = new(new Uri(ApiUrl.GetRiotEntitlementServiceUrl()));

            string response = client.Execute(
                new RestRequest(Method.POST)
                .AddHeader("Authorization", $"Bearer {Account.AccessToken}")
                .AddJsonBody("{}"))
            .Content;
            var entitlement_token = JsonConvert.DeserializeObject(response);
            JToken entitlement_tokenObj = JObject.FromObject(entitlement_token);

            return entitlement_tokenObj["entitlements_token"].Value<string>();
        }
    }
}
