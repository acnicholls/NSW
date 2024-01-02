namespace NSW.Data.DTO.Request
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public int LanguagePreference { get; set; }
    }
}
