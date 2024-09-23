using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class SignUpViewModel
    {
        
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
