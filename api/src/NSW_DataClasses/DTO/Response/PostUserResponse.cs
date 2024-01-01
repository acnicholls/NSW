namespace NSW.Data.DTO.Response
{
	public class PostUserResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public PostalCodeResponse PostalCode { get; set; }

		public int LanguagePreference { get; set; }
	}
}
