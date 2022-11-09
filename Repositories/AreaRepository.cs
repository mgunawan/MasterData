using MasterData.Repositories.Interface;
using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AreaDb _db;
        private readonly AreaCache _cache;
        public AreaRepository(AreaDb areaDb, AreaCache areaCache)
        {
            _db = areaDb ?? throw new ArgumentNullException(nameof(areaDb));
            _cache = areaCache ?? throw new ArgumentNullException(nameof(areaCache));
        }
        public AreaCache cache()
        {
            return _cache;
        }

        public AreaDb db()
        {
            return _db;
        }
    }
}
