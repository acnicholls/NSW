using NSW.Data.Interfaces;

namespace NSW.Data
{

	public class User : IUser
	{
		public User() { }
		public User(string username, string email)
		{
			this.Email = email;
			this.UserName = username;
		}
		public int Id { get; set; }
		public int IdpId { get; set; }
		public string Phone { get; set; }
		public string PostalCode { get; set; }

		public LanguagePreference DisplayLanguage { get; set; }

		public string UserName { get ; set ; }
		public string Email { get ; set ; }
	}
}
