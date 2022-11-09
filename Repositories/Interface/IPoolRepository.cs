using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;
//using MasterData.Repositories.Grpc;

namespace MasterData.Repositories.Interface
{
    public interface IPoolRepository {
        PoolDb db();
        PoolCache cache();
        //CompanyClient grpc();
    }
}
