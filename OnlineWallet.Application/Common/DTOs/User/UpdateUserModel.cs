﻿using OnlineWallet.Domain.Exceptions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.User
{
    public class UpdateUserModel
    {
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(8)]
        [PasswordPropertyText]
        public string? Password { get; set; }

        [RegularExpression("[0|1]", ErrorMessage = ErrorMessages.RoleOutOfBounds)]
        public int? Role { get; set; }
    }
}
