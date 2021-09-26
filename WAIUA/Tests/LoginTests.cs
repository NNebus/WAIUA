using System.Collections.Generic;
using System.Linq;
using System.Net;
using WAIUA.Models;
using WAIUA.Services;
using Xunit;

namespace WAIUA.Tests
{
    public class LoginTests
    {

        private static Account GetAccount() {
            Account account = new(new CookieContainer()) { Username = "", Password = "" }; // Add your Credentials

            if (string.IsNullOrEmpty(account.AccessToken))
            {
                account.GetTokens();
            }
                
            return account;
        }

        [Fact]
        public void GetJwtToken()
        {
            Account account = GetAccount();
            Assert.False(string.IsNullOrEmpty(account.AccessToken));
        }

        [Fact]
        public void GetPlayer() {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);
            Player player = valorantApiService.GetPlayer();

            Assert.False(string.IsNullOrEmpty(player.NameWithTag));
        }

        [Fact]
        public void GetLocalRegion() {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);
            string localRegion = valorantApiService.GetLocalRegion();

            Assert.False(string.IsNullOrEmpty(localRegion));
        }


        [Fact]
        public void GetCurrentMatchId()
        {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);
            string matchId = valorantApiService.GetCurrentMatchId();

            Assert.False(string.IsNullOrEmpty(matchId));
        }

        [Fact]
        public void GetPlayerMatchHistory()
        {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);
            string matchId = valorantApiService.GetCurrentMatchId();

            valorantApiService.GetMatchData(matchId);

            Assert.False(string.IsNullOrEmpty(matchId));
        }

        
        [Fact]
        public void GetPlayerMatchData()
        {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);
            List<Match> matches = new();

            List<string> matchIds = new() {
               // Add your MatchId's
            };

            foreach (string matchId in matchIds) {
                MatchHistoryEntry match = new()
                {
                    Id = matchId
                };

                matches.Add(valorantApiService.GetMatchResultOfMatchHistoryEntry(match.Id));
            }

            Assert.False(matches.First() == null);
        }

        [Fact]
        public void GetRankedHistoryForPlayer()
        {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);

            List<RankedMatch> rankedMatchHistory = valorantApiService.GetRankedHistoryForPlayer(account.UniqueId);

            Assert.False(rankedMatchHistory.Count() == 0);
        }

        [Fact]
        public void GetRankFromRankedHistory() {
            Account account = GetAccount();
            ValorantApiService valorantApiService = new(account);

            List<RankedMatch> rankedMatchHistory = valorantApiService.GetRankedHistoryForPlayer(account.UniqueId);

            Rank rank = valorantApiService.GetRankFromPlayer(rankedMatchHistory);

            Assert.NotNull(rank);
        }
    }
}
