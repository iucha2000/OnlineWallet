using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Application.Common.Handlers
{
    public interface ITokenHandler
    {
        string GenerateToken(User user);
    }
}
