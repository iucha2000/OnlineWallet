using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineWallet.Application.Common.Models.Wallet;

namespace OnlineWallet.Application.Common.Models.User
{
    public class GetUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<GetWalletModel> Wallets { get; set; }
    }
}
