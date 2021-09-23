using System.Net;
using WAIUA.Models;
using WAIUA.Services;
using Xunit;

namespace WAIUA.Tests
{
    public class LoginTests
    {

        private static Account GetAccount() {
            Account account = new(new CookieContainer()) { Username = "", Password = "" };

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
            MatchHistoryEntry match = new() {
                Id = "",  
            };

            Match res = valorantApiService.GetMatchResultOfMatchHistoryEntry(match.Id);

            Assert.False(res == null);
        }
    }
}
