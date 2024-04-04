using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Exceptions
{
    public class NoEnoughBalanceException : Exception
    {
        public NoEnoughBalanceException(string message) : base(message) { }
    }
}
