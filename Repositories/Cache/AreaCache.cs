using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{
    public partial class AreaCache
    {
        private readonly IDistributedCache _cache;

        public AreaCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<List<Models.Area>> GetById()
        {
            var o = await _cache.GetStringAsync("MD.BB1.ListAreaPool");
            return JsonConvert.DeserializeObject<List<Models.Area>>(o);
        }
        public async Task<Models.Area> GetById(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.AreaPool.{id}");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.Area>(o);
        }

        public async Task<bool> SetCache(Models.Area o)
        {
            await _cache.SetStringAsync($"MD.BB1.AreaPool.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<bool> SetCache(List<Models.Area> o)
        {
            await _cache.SetStringAsync("MD.BB1.ListAreaPool", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<bool> SetCache(string key, Models.Area o)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(o));
            return true;
        }

        public async Task<bool> DeleteCache()
        {
            await _cache.RemoveAsync("MD.BB1.ListAreaPool");
            return true;
        }

        public async Task<List<Models.Area>> GetAll()
        {
            var o = await _cache.GetStringAsync($"MD.BB1.ListAreaPool");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.Area>>(o);
        }
    }
}
