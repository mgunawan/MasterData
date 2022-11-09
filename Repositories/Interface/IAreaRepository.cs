using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories.Interface
{
    public interface IAreaRepository {
        AreaDb db();
        AreaCache cache();
    }
}
