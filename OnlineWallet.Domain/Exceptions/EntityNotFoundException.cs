using OnlineWallet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
