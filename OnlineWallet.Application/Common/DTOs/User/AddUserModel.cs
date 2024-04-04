using OnlineWallet.Domain.Exceptions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.User
{
    public class AddUserModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [RegularExpression("[0|1]", ErrorMessage = ErrorMessages.RoleOutOfBounds)]
        public int Role { get; set; }
    }
}
