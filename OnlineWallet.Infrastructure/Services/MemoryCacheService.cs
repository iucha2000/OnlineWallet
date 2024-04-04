using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OnlineWallet.Application.Services;
using OnlineWallet.Application.Services.Models;

namespace OnlineWallet.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CachingOptionsModel _cachingOptionsModel;

        public MemoryCacheService(IMemoryCache memoryCache, IOptions<CachingOptionsModel> options)
        {
            _memoryCache = memoryCache;
            _cachingOptionsModel = options.Value;
        }

        public T GetData<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void RemoveData(string key)
        {
            _memoryCache.Remove(key);
        }

        public bool SetData<T>(string key, T value, MemoryCacheEntryOptions options = null)
        {
            try
            {
                if (options == null)
                {
                    options = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(_cachingOptionsModel.AbsoluteExpiration),
                        SlidingExpiration = TimeSpan.FromMinutes(_cachingOptionsModel.SlidingExpiration),
                        Priority = CacheItemPriority.Normal,
                        Size = _cachingOptionsModel.Size,
                    };
                }

                _memoryCache.Set(key, value, options);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
