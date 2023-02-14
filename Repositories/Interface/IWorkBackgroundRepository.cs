using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories.Interface
{
    public interface IWorkBackgroundRepository
    {
        WorkBackgroundDb db();
        WorkBackgroundCache cache();
    }
}
