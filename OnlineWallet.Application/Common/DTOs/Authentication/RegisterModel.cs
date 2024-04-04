using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Authentication
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
