using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{

    public class CompanyCache
    {
        private readonly IDistributedCache _cache;

        public CompanyCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<bool> SetCache(Models.Company o)
        {
            await _cache.SetStringAsync($"MD.BB1.Company.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<bool> SetCache(List<Models.Company> o)
        {
            await _cache.SetStringAsync("MD.BB1.ListCompany", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<Models.Company> GetCache(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.Company.{id}");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.Company>(o);
        }

        public async Task<List<Models.Company>> GetCache()
        {
            var o = await _cache.GetStringAsync("MD.BB1.ListCompany");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.Company>>(o);
        }

        public async Task<bool> DeleteCahce(string id)
        {
            await _cache.RemoveAsync($"MD.BB1.Company.{id}");
            return true;
        }
    }
}
