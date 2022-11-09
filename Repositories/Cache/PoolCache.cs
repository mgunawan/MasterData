using MasterData.Models.Dummy;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{
    public class PoolCache
    {
        private readonly IDistributedCache _cache;

        public PoolCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Models.Pool> GetById(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.Pool.{id}");

            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.Pool>(o);
        }

        public async Task<bool> RemovePoolById(string id)
        {
            await _cache.RemoveAsync($"MD.BB1.Pool.{id}");
            return true;
        }

        public async Task<bool> SetCache(Models.Pool o)
        {
            if (o != null)
                await _cache.SetStringAsync($"MD.BB1.Pool.{o.Id}", JsonConvert.SerializeObject(o));
            return true;
        }

        public async Task<bool> SetPoolCompanyService(List<PoolCompany> o)
        {
            await _cache.SetStringAsync($"PoolCompanyService.{o[0].PoolId}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<List<PoolCompany>> GetPoolCompanyService(string poolId)
        {
            var o = await _cache.GetStringAsync($"PoolCompanyService.{poolId}");
            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<PoolCompany>>(o)!;
        }

        public async Task<Models.Pool> GetPoolById(string id)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.Pool.{id}");
            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<Models.Pool>(o)!;
        }

        public async Task<bool> SetPools(List<Models.Pool> oList)
        {
            await _cache.SetStringAsync($"MD.BB1.Pools", JsonConvert.SerializeObject(oList), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
            return true;
        }

        public async Task<List<Models.Pool>> GetPools()
        {
            var o = await _cache.GetStringAsync($"MD.BB1.Pools");
            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.Pool>>(o)!;
        }

        public async Task<bool> SetPoolByAreaId(List<Models.Pool> oList)
        {
            await _cache.SetStringAsync($"MD.BB1.AreaAll.{oList[0].AreaId}", JsonConvert.SerializeObject(oList));
            return true;
        }

        public async Task<List<Models.Pool>> GetPoolByAreaId(string areaId)
        {
            var o = await _cache.GetStringAsync($"MD.BB1.AreaAll.{areaId}");
            if (string.IsNullOrEmpty(o))
                return null;

            return JsonConvert.DeserializeObject<List<Models.Pool>>(o)!;
        }

        public async Task<bool> DeleteCahce(Models.Pool pool)
        {
            await _cache.RemoveAsync("MD.BB1.Pools");
            await _cache.RemoveAsync($"MD.BB1.Pool.{pool.Id}");
            await _cache.RemoveAsync($"PoolCompanyService.{pool.Id}");

            return true;
        }
    }
}
