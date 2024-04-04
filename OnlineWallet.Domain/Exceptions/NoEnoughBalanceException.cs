namespace OnlineWallet.Domain.Exceptions
{
    public class NoEnoughBalanceException : Exception
    {
        public NoEnoughBalanceException(string message) : base(message) { }
    }
}
