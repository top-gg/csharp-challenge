using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Earthquake.Infrastructure.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T data,
            TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration, CancellationToken token = default)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(60),
                SlidingExpiration = slidingExpiration
            };
            await distributedCache.SetStringAsync(key, jsonData, options, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key,
            CancellationToken token = default) where T : class
        {
            var jsonData = await distributedCache.GetStringAsync(key, token);
            return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}