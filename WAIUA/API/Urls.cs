namespace WAIUA.Api {
    public static class ApiUrl
    {
        public const string ValorantApi = "https://valorant-api.com/v1/version";
        public const string ValorantOptInUrl = "https://playvalorant.com/opt_in";
        public const string RiotAuthorizationServiceUrl = "https://auth.riotgames.com/api/v1/authorization";
        public const string RiotEntitlementServiceUrl = "https://entitlements.auth.riotgames.com/api/token/v1";
        public const string RiotUserInfoServiceUrl = "https://auth.riotgames.com/userinfo";
        public const string RiotPlayerServiceUrl = "https://pd.{###Region###}.a.pvp.net/name-service/v2/players"; //{###Region###} Needs to be replaced with Region-Code (f.e. "eu").
        public const string RiotMatchServiceUrl = "https://glz-{###Shard###}-1.{###Region###}.a.pvp.net/core-game/v1/players/{###PUUID###}"; //{###Shard###}, {###Region###} {###PUUID###} and  Needs to be replaced
        public const string RiotMatchServiceUrl2 = "https://glz-{###Shard###}-1.{###Region###}.a.pvp.net/core-game/v1/matches/{###MatchId###}"; //{###Shard###}, {###Region###} {###MatchId###} and  Needs to be replaced
        public const string RiotMatchHistorySericeUrl = "https://pd.{###Region###}.a.pvp.net/mmr/v1/players/{###PUUID###}/competitiveupdates?queue=competitive";
    }
}