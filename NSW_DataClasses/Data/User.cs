using NSW;
using NSW.Data.Interfaces;


namespace NSW.Data
{

	public class User : IUser
	{
		public User(string name, string email)
		{
			this.ID = 0;
			this.Name = name;
			this.Email = email;
			this.Password = "passw0rd!";
			this.Phone = "";
			this.PostalCode = "12345";
			this.Role = "Member";
			this.LanguagePreference = 1;
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string PostalCode { get; set; }
		public string Role { get; set; }

		public int LanguagePreference { get; set; }

		public Role UserRole
		{
			get
			{
				var returnValue = new Role();
				switch (Role)
				{
					case "ADMIN":
						{
							returnValue = NSW.Role.Admin; break;
						}
					case "MEMBER":
						{
							returnValue = NSW.Role.Member; break;
						}
				}
				return returnValue;
			}
		}

		public LanguagePreference DisplayLanguage
		{
			get
			{
				switch (LanguagePreference)
				{
					case 2:
						return NSW.LanguagePreference.English;
					case 1:
						return NSW.LanguagePreference.Japanese;
					default:
						return NSW.LanguagePreference.Japanese;
				}
			}
		}

		public User()
		{
		}




	}
}
