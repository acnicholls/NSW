using System.ComponentModel.DataAnnotations;

namespace NSW.Idp.Models.Account
{
    public class NewUserModel
    {
		[MaxLength(255)]
		public string FirstName { get; set; }
		
		[MaxLength(255)]
		public string LastName { get; set; }
        
		[Required]
        [MaxLength(100, ErrorMessage = "Your username cannot be longer than 100 characters, sorry.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(52, ErrorMessage = "Your email address cannot be longer than 52 characters, sorry.")]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "The Password and Confirmation Password do not match.")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "The Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
		public string ReturnUrl { get; set; }

		[Required]
		[MaxLength(8, ErrorMessage ="The Postal Code cannot exceed 8 characters.")]
		public string PostalCode { get; set; }

		[Required]
		public LanguagePreference LanguagePreference { get; set; }

		[MaxLength(15, ErrorMessage ="The Phone Number cannot exceed 15 characters.")]
		public string PhoneNumber { get; set; }
    }
}