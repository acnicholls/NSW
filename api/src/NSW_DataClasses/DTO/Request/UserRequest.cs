using NSW.Data.Validation;
using System.ComponentModel.DataAnnotations;


namespace NSW.Data.DTO.Request
{
    public class UserRequest
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        [NaganoPostalCodeValidation(ErrorMessage = "Please enter a valid Nagano Postal Code.")]
        public string PostalCode { get; set; }
        public int LanguagePreference { get; set; }
    }
}
