using NSW;

namespace NSW.Data.Interfaces
{
	public interface IUser
	{
		int Id { get; set; }
		string UserName { get; set; }
		string Email { get; set; }
		string Phone { get; set; }
        string Role { get; set; }
		string PostalCode { get; set; }
		int LanguagePreference { get; }
	}
}
