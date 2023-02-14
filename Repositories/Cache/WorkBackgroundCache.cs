using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{
    public class WorkBackgroundCache
    {
        private readonly IDistributedCache _cache;

        public WorkBackgroundCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Models.WorkBackground> GetById(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.WB.{id}");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.WorkBackground>(o);
        }

        public async Task<bool> SetCache(Models.WorkBackground o)
        {
            await _cache.SetStringAsync($"MD.BB1.WB.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
            return true;
        }

        public async Task<bool> SetCache(List<Models.WorkBackground> o)
        {
            await _cache.SetStringAsync($"MD.BB1.List.WB", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
            return true;
        }

        public async Task<bool> DeleteCache()
        {
            await _cache.RemoveAsync("MD.BB1.List.WB");
            return true;
        }

        public async Task<List<Models.WorkBackground>> GetAll()
        {
            var o = await _cache.GetStringAsync("MD.BB1.List.WB");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.WorkBackground>>(o);
        }
    }
}
