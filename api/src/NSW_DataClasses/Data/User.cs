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
		public string UserName { get ; set ; }
		public string Email { get ; set ; }
		public string Phone { get; set; }
        public string Role { get; set; }
		public string PostalCode { get; set; }
		public int LanguagePreference { get; set; }

	}
}
