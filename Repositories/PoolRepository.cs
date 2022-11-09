using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;
//using MasterData.Repositories.Grpc;

namespace MasterData.Repositories
{
    public class PoolRepository : IPoolRepository
    {
        private readonly PoolDb _db;
        private readonly PoolCache _cache;
        //private readonly CompanyClient _grpc;

        public PoolRepository(PoolDb poolDb, PoolCache poolCache) //, CompanyClient compGrpc)
        {
            _db = poolDb ?? throw new ArgumentNullException(nameof(poolDb));
            _cache = poolCache ?? throw new ArgumentNullException(nameof(poolCache));
            //_grpc = compGrpc ?? throw new ArgumentNullException(nameof(compGrpc));
        }
        public PoolCache cache()
        {
            return _cache;
        }

        public PoolDb db()
        {
            return _db;
        }

        //public CompanyClient grpc()
        //{
        //    return _grpc;
        //}
    }
}
