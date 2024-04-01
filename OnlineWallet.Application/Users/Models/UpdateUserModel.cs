﻿using OnlineWallet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Users.Models
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
    }
}