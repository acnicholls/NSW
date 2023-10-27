using NSW;

namespace NSW.Data.Interfaces
{
	public interface IUser
	{
		LanguagePreference DisplayLanguage { get; }
		 int ID { get; set; }
		 string Name { get; set; }
		 string Password { get; set; }
		 string Email { get; set; }
		 string Phone { get; set; }
		 string PostalCode { get; set; }
	}
}
