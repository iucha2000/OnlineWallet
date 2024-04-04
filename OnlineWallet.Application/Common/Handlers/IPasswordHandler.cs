namespace OnlineWallet.Application.Common.Handlers
{
    public interface IPasswordHandler
    {
        void CreateHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
