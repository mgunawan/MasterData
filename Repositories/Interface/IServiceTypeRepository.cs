using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories.Interface
{
    public interface IServiceTypeRepository
    {
        ServiceTypeDb db();
        ServiceTypeCache cache();
    }
}
