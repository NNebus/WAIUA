namespace WAIUA.Api {

    //https://github.com/RumbleMike/ValorantClientAPI/tree/master/Docs
    //https://github.com/RumbleMike/ValorantClientAPI/pull/21/commits/2f03680b444f543bc64adaf9be20f5331c562503?short_path=68655f3#diff-68655f3b5d11f473de824a71cbcdc30438d3fa15ea5e89b35cb157b5bf725d6f
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
        public const string RiotMatchHistorySericeUrl = "https://pd.{###Region###}.a.pvp.net/match-history/v1/history/{###PUUID###}?startIndex=0&endIndex=10";
        public const string RiotMatchResultSericeUrl = "https://pd.{###Region###}.a.pvp.net/match-details/v1/matches/{###MatchId###}";
    }
}