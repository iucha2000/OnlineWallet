using OnlineWallet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Abstractions.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<Result<T>> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "", bool trackChanges = true);
        Task<Result<IList<T>>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int count = 0, bool trackChanges = true);
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result> InsertAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
        Task<Result> DeleteByIdAsync(int id);
    }
}
