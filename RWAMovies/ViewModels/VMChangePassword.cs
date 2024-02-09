using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RWAMovies.ViewModels
{
    public class VMChangePassword
    {
        public string Username { get; set; }

        [DisplayName("Old password")]
        [PasswordPropertyText]
        public string OldPassword { get; set; }

        [DisplayName("New password")]
        [PasswordPropertyText]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        [DisplayName("New password confirm")]
        [PasswordPropertyText]
        public string NewPasswordConfirm { get; set; }
    }
}
