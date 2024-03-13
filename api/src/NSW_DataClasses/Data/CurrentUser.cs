using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSW.Data.Interfaces;

namespace NSW.Data
{

    public class CurrentUser : IUser
    {
        private readonly ClaimsPrincipal? _claimsPrincipal;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this._claimsPrincipal = httpContextAccessor?.HttpContext?.User;
            this.Id = Convert.ToInt32(this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "sub")?.Value);
            this.UserName = this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? string.Empty;
            this.Email = this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "email")?.Value ?? string.Empty;

            this.Phone = this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "phone_number")?.Value ?? string.Empty;
            this.Role = this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "role")?.Value ?? "MEMBER";
            this.PostalCode = this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "postal_code")?.Value ?? string.Empty;

            this.LanguagePreference = Convert.ToInt32(this._claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == "language_preference")?.Value);
        }


        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string PostalCode { get; set; }
        public int LanguagePreference { get; set; }

    }
}
