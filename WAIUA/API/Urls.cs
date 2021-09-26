namespace WAIUA.Api {

    //https://github.com/RumbleMike/ValorantClientAPI/tree/master/Docs
    //https://github.com/RumbleMike/ValorantClientAPI/pull/21/commits/2f03680b444f543bc64adaf9be20f5331c562503?short_path=68655f3#diff-68655f3b5d11f473de824a71cbcdc30438d3fa15ea5e89b35cb157b5bf725d6f
    public static class ApiUrl
    {
        public static string GetValorantApiUrl() => "https://valorant-api.com/v1/version";
        public static string GetValorantOptInApiUrl() => "https://playvalorant.com/opt_in";
        public static string GetRiotAuthorizationServiceUrl() => "https://auth.riotgames.com/api/v1/authorization";
        public static string GetRiotEntitlementServiceUrl() => "https://entitlements.auth.riotgames.com/api/token/v1";
        public static string GetRiotUserInfoServiceUrl() => "https://auth.riotgames.com/userinfo";
        public static string GetRiotPlayerServiceUrl(string region) => $"https://pd.{region}.a.pvp.net/name-service/v2/players";
        public static string GetRiotMatchServiceUrl(string region, string shard, string playerId) => $"https://glz-{shard}-1.{region}.a.pvp.net/core-game/v1/players/{playerId}";
        public static string GetRiotMatchServiceUrl2(string region, string shard, string matchId) => $"https://glz-{shard}-1.{region}.a.pvp.net/core-game/v1/matches/{matchId}";
        public static string GetRiotMatchHistoryServiceUrl(string region, string playerId, int amountMatches = 10) => $"https://pd.{region}.a.pvp.net/match-history/v1/history/{playerId}?startIndex=0&endIndex={amountMatches}";
        public static string GetRiotMatchResultServiceUrl(string region, string matchId) => $"https://pd.{region}.a.pvp.net/match-details/v1/matches/{matchId}";
        public static string GetRiotRankedMatchServiceUrl(string region, string playerId) => $"https://pd.{region}.a.pvp.net/mmr/v1/players/{playerId}/competitiveupdates?queue=competitive";


    }
}