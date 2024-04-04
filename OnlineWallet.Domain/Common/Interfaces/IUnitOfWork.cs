namespace OnlineWallet.Domain.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<bool> CommitAsync();
    }
}
