using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{
    public partial class MediaCache
    {
        private readonly IDistributedCache _cache;

        public MediaCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<bool> SetCache(Models.Media o)
        {
            await _cache.SetStringAsync($"MD.BB1.Media.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
            return true;
        }

        public async Task<bool> SetCache(List<Models.Media> o)
        {
            await _cache.SetStringAsync($"MD.BB1.Media", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
            return true;
        }

        public async Task<Models.Media> GetById(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.Media.{id}");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.Media>(o);
        }

        public async Task<List<Models.Media>> GetAll()
        {
            var o = await _cache.GetStringAsync("MD.BB1.Media");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.Media>>(o);
        }
    }
}
