using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories.Interface
{
    public interface IMediaRepository
    {
        MediaDb db();
        MediaCache cache();
    }
}
