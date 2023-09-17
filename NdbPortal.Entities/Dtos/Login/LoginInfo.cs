using System.ComponentModel.DataAnnotations;

namespace NdbPortal.Entities.Dtos.Login
{
    public class LoginInfo
    {
        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
    }
}
