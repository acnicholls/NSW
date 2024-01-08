namespace NSW.Data.DTO.Response
{
	public class UserResponse
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Role { get; set; }
		public PostalCodeResponse PostalCode { get; set; }
		public int LanguagePreference { get; set; }
		public bool IsAuthenticated { get; set; } = false;
	}
}
