﻿using OnlineWallet.Domain.Common.Interfaces;

namespace OnlineWallet.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private ApplicationDbContext _context;
        private readonly Func<ApplicationDbContext> _contextFactory;

        public ApplicationDbContext DbContext => _context ??= _contextFactory.Invoke();

        public UnitOfWork(Func<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> CommitAsync()
        {
            return await DbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            if (_disposed == false && _context != null)
            {
                _context.Dispose();
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
