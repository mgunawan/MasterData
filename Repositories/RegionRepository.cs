using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;
using System;

namespace MasterData.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly RegionDb _regionDb;
        private readonly RegionCache _regionCache;
        
        public RegionRepository(RegionDb regionDb, RegionCache regionCache)
        {
            _regionDb = regionDb ?? throw new ArgumentNullException(nameof(RegionDb));
            _regionCache = regionCache ?? throw new ArgumentNullException(nameof(RegionCache));
        }

        public RegionCache cache() => _regionCache;
        
        public RegionDb db() => _regionDb;
    }
}