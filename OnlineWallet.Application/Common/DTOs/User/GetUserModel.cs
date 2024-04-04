using OnlineWallet.Application.Common.DTOs.Wallet;

namespace OnlineWallet.Application.Common.DTOs.User
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
