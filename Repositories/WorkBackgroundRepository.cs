using MasterData.Repositories.Interface;
using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories
{
    public class WorkBackgroundRepository : IWorkBackgroundRepository
    {
        private readonly WorkBackgroundDb _db;
        private readonly WorkBackgroundCache _cache;
        public WorkBackgroundRepository(WorkBackgroundDb Db, WorkBackgroundCache Cache)
        {
            _db = Db ?? throw new ArgumentNullException(nameof(Db));
            _cache = Cache ?? throw new ArgumentNullException(nameof(Cache));
        }
        public WorkBackgroundCache cache()
        {
            return _cache;
        }

        public WorkBackgroundDb db()
        {
            return _db;
        }
    }
}
