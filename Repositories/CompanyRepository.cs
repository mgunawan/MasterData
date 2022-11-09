using MasterData.Repositories.Cache;
using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyDb _db;
        private readonly CompanyCache _cache;
        public CompanyRepository(CompanyDb companyDb, CompanyCache companyCache)
        {
            _db = companyDb ?? throw new ArgumentNullException(nameof(companyDb));
            _cache = companyCache ?? throw new ArgumentNullException(nameof(companyCache));
        }
        public CompanyCache cache()
        {
            return _cache;
        }

        public CompanyDb db()
        {
            return _db;
        }
    }
}
