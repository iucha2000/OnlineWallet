using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Exceptions
{
    public static class ErrorMessages
    {
        public const string NotFoundMessage = "Not found";
        public const string UserAlreadyExists = "User already exists";
        public const string RoleOutOfBounds = "Role parameter must be equal to 0 or 1";
        public const string EmailAlreadyExists = "User with given Email already exists";
        public const string IncorrectCredentials = "User with given credentials does not exist";
        public const string UserNotFound = "User with given ID does not exist";
    }
}
