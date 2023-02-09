using Dapper;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace MasterData.Repositories.MySql
{
    public class MediaDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public MediaDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> Create(Models.Media o)
        {
            var dt = DateTime.UtcNow;
            string findQuery = "SELECT name from mst_media where id = @id";
            var oData = await _connection.QuerySingleOrDefaultAsync<Models.Media>(findQuery, new { id = o.Id });

            string sqlQuery = @"INSERT into mst_media (id, name, description, is_active, index_cmb) VALUES 
                                (@id, @name, @description, @is_active, @index_cmb)";
            string updateQuery = @"UPDATE mst_media set name = @name, description = @description, is_active = @is_active, 
                                index_cmb = @index_cmb where id = @id";
            int res = 0;
            if (oData == null)
            {
                res = await _connection.ExecuteAsync(sqlQuery, new
                {
                    id = o.Id,
                    name = o.Name,
                    description = o.Description,
                    is_active = true,
                    index_cmb = o.IndexCmb,
                });
            }
            else
            {
                res = await _connection.ExecuteAsync(updateQuery, new
                {
                    id = o.Id,
                    name = o.Name,
                    description = o.Description,
                    is_active = true,
                    index_cmb = o.IndexCmb,
                });
            }

            return res > 0;
        }

        public async Task<Models.Media> GetById(string id)
        {
            string sqlQuery = "SELECT id, name, description, is_active, index_cmb from mst_media where id = @idm and is_active = 1";
            var oData = await _connection.QuerySingleOrDefaultAsync<Models.Media>(sqlQuery, new { idm = id });
            return oData;
        }

        public async Task<List<Models.Media>> GetAll()
        {
            string sqlQuery = "SELECT id, name, description, is_active, index_cmb from mst_media where is_active = 1 order by index_cmb asc";
            var oData = await _connection.QueryAsync<Models.Media>(sqlQuery);
            return (List<Models.Media>)oData;
        }
    }
}
