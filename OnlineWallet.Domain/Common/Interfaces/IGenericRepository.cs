using System.Linq.Expressions;

namespace OnlineWallet.Domain.Common.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<Result<T>> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "", bool trackChanges = true);
        Task<Result<IList<T>>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool trackChanges = true);
        Task<Result<IList<T>>> GetAllAsync();
        Task<Result<T>> GetByIdAsync(Guid id);
        Task<Result> InsertAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
        Task<Result> DeleteByIdAsync(Guid id);
    }
}
