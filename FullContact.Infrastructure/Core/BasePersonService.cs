using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullContact.Infrastructure
{
    public abstract class BasePersonService
    {
        private readonly IMemoryCache _memoryCache;
        protected string BaseUrl { get; private set; }

        protected string ApiKey { get; private set; }

        public string Name => _name;

        private string _name;

        protected BasePersonService(string name, string baseUrl, string apiKey = null)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            ApiKey = apiKey;
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public Task<IPersonResponse> GetPerson(string identifier)
        {
            return _memoryCache.GetOrCreateAsync(identifier, x => Get(identifier));
        }

        public abstract Task<IPersonResponse> Get(string identifier);
    }
}
