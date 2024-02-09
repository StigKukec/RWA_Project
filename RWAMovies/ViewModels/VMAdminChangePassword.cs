using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RWAMovies.ViewModels
{
    public class VMAdminChangePassword
    {
        public int UserId { get; set; }

        [DisplayName("New password")]
        [PasswordPropertyText]
        public string NewPassword { get; set; }
    }
}
