using Dapper;
using MySql.Data.MySqlClient;

namespace MasterData.Repositories.MySql
{
    public class AreaDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public AreaDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> Create(Models.Area o)
        {
            string sqlQuery = "INSERT into mst_area (id, name) VALUES (@id, @name)";
            var res = await _connection.ExecuteAsync(sqlQuery, new { id = o.Id, name = o.Name });
            return res > 0;
        }

        public async Task<List<Models.Area>> GetAll()
        {
            string sqlQuery = "SELECT id, name from mst_area";
            var res = await _connection.QueryAsync<Models.Area>(sqlQuery);
            return (List<Models.Area>)res;
        }

        public async Task<Models.Area> GetById(string id)
        {
            string sqlQuery = "SELECT id, name from mst_area where id = @id";
            var res = await _connection.QuerySingleOrDefaultAsync<Models.Area>(sqlQuery, new { id = id });
            return (Models.Area)res;
        }
    }
}
