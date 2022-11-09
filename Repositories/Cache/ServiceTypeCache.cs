using MasterData.Models.VM;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MasterData.Repositories.Cache
{
    public class ServiceTypeCache
    {
        private readonly IDistributedCache _cache;

        public ServiceTypeCache(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<bool> SetCache(Models.VM.ServiceType o)
        {
            if (o != null)
                await _cache.SetStringAsync($"VM.BB1.ServiceType.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                });
            return true;
        }

        public async Task<Models.VM.ServiceType> GetCache(string id)
        {
            var o = await _cache.GetStringAsync($"VM.BB1.ServiceType.{id}");
            return JsonConvert.DeserializeObject<Models.VM.ServiceType>(o);
        }

        public async Task<bool> SetListCache(List<Models.VM.ServiceType> o)
        {
            if (o != null)
                await _cache.SetStringAsync($"VM.BB1.lServiceType", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                });
            return true;
        }

        public async Task<List<Models.VM.ServiceType>> GetListCache()
        {
            var o = await _cache.GetStringAsync($"VM.BB1.lServiceType");
            return JsonConvert.DeserializeObject<List<Models.VM.ServiceType>>(o);
        }

        public async Task<bool> DeleteCache()
        {
            await _cache.RemoveAsync("VM.BB1.lServiceType");
            return true;
        }

        public async Task<ServiceType> GetByPoolAndId(string pool_id, string service_id)
        {
            var o = await _cache.GetStringAsync($"VM.BB1.ServiceType.{pool_id}.{service_id}");
            return JsonConvert.DeserializeObject<Models.VM.ServiceType>(o);
        }

        public async Task<bool> SetCachePoolAndId(string pool_id, ServiceType o)
        {
            if (o != null)
                await _cache.SetStringAsync($"VM.BB1.ServiceType.{pool_id}.{o.Id}", JsonConvert.SerializeObject(o), new DistributedCacheEntryOptions 
                { 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) 
                });
            return true;
        }
    }
}
