using System;
using System.ComponentModel.DataAnnotations;
namespace belt_project.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(2, ErrorMessage = "First Name must have at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must be alphabetical characters only.")]
        public string First { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(2, ErrorMessage = "Last Name must have at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must be alphabetical characters only.")]
        public string Last { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters.")]
        [RegularExpression(@"^(?=.*[^\da-zA-Z])(?=.*\d).+$", ErrorMessage = "Password requires at least one number and special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation required")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}