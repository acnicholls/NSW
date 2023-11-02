using System.ComponentModel.DataAnnotations;

namespace Starter.Idp.Models
{
    public class NewUserModel
    {
        [Required]
        [MaxLength(100, ErrorMessage="Your username cannot be longer than 100 characters, sorry.")]
        public string UserName {get;set;}

        [Required]
        [EmailAddress]
        [MaxLength(52, ErrorMessage="Your email address cannot be longer than 52 characters, sorry.")]
        public string Email {get;set;}

        [Required]
        [Compare(nameof(ConfirmPassword), ErrorMessage="The Password and Confirmation Password do not match.")]
        public string Password {get;set;}
        
        [Required]
        [Compare(nameof(Password), ErrorMessage="The Password and Confirmation Password do not match.")]
        public string ConfirmPassword {get;set;}

    }
}