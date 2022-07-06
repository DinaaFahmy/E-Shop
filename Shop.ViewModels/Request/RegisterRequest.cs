using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [MinLength(10)]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }
    }
}
