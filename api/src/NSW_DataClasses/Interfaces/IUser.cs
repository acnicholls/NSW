using NSW;

namespace NSW.Data.Interfaces
{
	public interface IUser
	{
		LanguagePreference DisplayLanguage { get; }
		int Id { get; set; }
		int IdpId { get; set; }
		string UserName { get; set; }
		string Email { get; set; }
		string Phone { get; set; }
		string PostalCode { get; set; }
	}
}
