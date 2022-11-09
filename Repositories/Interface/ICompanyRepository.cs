using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories.Interface
{
    public interface ICompanyRepository
    {
        CompanyDb db();
        CompanyCache cache();
    }
}
