using MasterData.Repositories.Interface;
using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly ServiceTypeDb _db;
        private readonly ServiceTypeCache _cache;
        public ServiceTypeRepository(ServiceTypeDb serviceDb, ServiceTypeCache serviceCache)
        {
            _db = serviceDb ?? throw new ArgumentNullException(nameof(serviceDb));
            _cache = serviceCache ?? throw new ArgumentNullException(nameof(serviceCache));
        }
        public ServiceTypeCache cache()
        {
            return _cache;
        }

        public ServiceTypeDb db()
        {
            return _db;
        }
    }
}
