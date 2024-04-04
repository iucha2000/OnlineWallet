using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Authentication
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
