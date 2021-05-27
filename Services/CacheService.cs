using Microsoft.Extensions.Caching.Memory;

using System;

using TestApp.Models;

namespace TestApp.Services
{
    public class CacheService
    {
        private readonly MemoryCache memoryCache;

        public CacheService(MemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        private string GetKey(Guid id) => id.ToString();

        public UserModel UpdateOrSet(Guid userId)
        {
            var key = GetKey(userId);
            
            if (!memoryCache.TryGetValue<UserModel>(key, out var data))
            {
                data = new UserModel()
                {
                    Id = userId,
                    Name = $"This is name {userId}"
                };
            }

            data.SomeRandomData = Guid.NewGuid().ToString();

            memoryCache.Set(key, data, TimeSpan.FromMinutes(25));

            return data;
        }

        public UserModel Get(Guid userId)
        {
            var key = GetKey(userId);

            return memoryCache.TryGetValue<UserModel>(key, out var value) 
                ? value 
                : UpdateOrSet(userId);
        }
    }
}
