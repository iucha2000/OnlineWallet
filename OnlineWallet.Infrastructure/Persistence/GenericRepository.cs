using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task<Result> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Result.Succeed();
        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity == null)
            {
                return Result.Failed(ErrorMessages.NotFoundMessage, StatusCodes.Status404NotFound);
            }
            return await DeleteAsync(entity);
        }

        public async Task<Result<T>> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "", bool trackChanges = true)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            var result = trackChanges ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
            return Result<T>.Succeed(result);
        }

        public async Task<Result<IList<T>>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int count = 0, bool trackChanges = true)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            if(count > 0)
            {
                query = query.Take(count);
            }

            var result = trackChanges ?  await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
            return Result<IList<T>>.Succeed(result);
        }

        public async Task<Result<T>> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FindAsync(id);
            return Result<T>.Succeed(result);
        }

        public async Task<Result> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return Result.Succeed();
        }

        public async Task<Result<T>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Result<T>.Succeed(entity);
        }
    }
}
