using OnlineWallet.Application.Wallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Users.Models
{
    public class GetUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public GetWalletModel[] Wallets { get; set; }
    }
}
