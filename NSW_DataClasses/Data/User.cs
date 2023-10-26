using NSW.Enums;
using NSW.Data.Interfaces;

namespace NSW.Data
{

	public class User : IUser
	{

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
							returnValue = NSW.Enums.Role.Admin; break;
						}
					case "MEMBER":
						{
							returnValue = NSW.Enums.Role.Member; break;
						}
				}
				return returnValue;
			}
		}

		public LanguagePreferenceEnum DisplayLanguage
		{
			get
			{
				switch (LanguagePreference)
				{
					case 2:
						return LanguagePreferenceEnum.English;
					case 1:
						return LanguagePreferenceEnum.Japanese;
					default:
						return LanguagePreferenceEnum.Japanese;
				}
			}
		}

		public User()
		{
		}




	}
}
