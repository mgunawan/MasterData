using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;

namespace MasterData.Repositories.Interface
{
    public interface IRegionRepository
    {
        RegionDb db();
        RegionCache cache();
    }
    
}