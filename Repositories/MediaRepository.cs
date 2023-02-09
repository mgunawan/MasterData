using MasterData.Repositories.Interface;
using MasterData.Repositories.Cache;
using MasterData.Repositories.MySql;

namespace MasterData.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly MediaDb _db;
        private readonly MediaCache _cache;
        public MediaRepository(MediaDb mediaDb, MediaCache mediaCache)
        {
            _db = mediaDb ?? throw new ArgumentNullException(nameof(mediaDb));
            _cache = mediaCache ?? throw new ArgumentNullException(nameof(mediaCache));
        }
        public MediaCache cache()
        {
            return _cache;
        }

        public MediaDb db()
        {
            return _db;
        }
    }
}
