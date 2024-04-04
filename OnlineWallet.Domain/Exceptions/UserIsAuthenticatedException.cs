namespace OnlineWallet.Domain.Exceptions
{
    public class UserIsAuthenticatedException : Exception
    {
        public UserIsAuthenticatedException(string message) : base(message) { }
    }
}
