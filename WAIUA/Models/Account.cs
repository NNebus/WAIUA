using System.Net;
using WAIUA.Services;

namespace WAIUA.Models
{
    public class Account {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string EntitlementToken { get; set; }

        private LoginService loginService { get; }

        public CookieContainer CookieContainer { get; set; }

        public string UniqueId { get; set; } //PUUID (https://riot-api-libraries.readthedocs.io/en/latest/ids.html)

        public Account(CookieContainer cookieContainer) {
            CookieContainer = cookieContainer;

            loginService = new LoginService(CookieContainer, this);

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)) {
                AccessToken = loginService.GetJwt();
                EntitlementToken = loginService.GetEntitlementToken();
            }
        }
        public void GetTokens() {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                AccessToken = loginService.GetJwt();
                EntitlementToken = loginService.GetEntitlementToken();
            }
        }
    }
}