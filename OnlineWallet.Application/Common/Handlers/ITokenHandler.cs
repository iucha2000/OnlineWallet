using OnlineWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Common.Handlers
{
    public interface ITokenHandler
    {
        string GenerateToken(User user);
    }
}
