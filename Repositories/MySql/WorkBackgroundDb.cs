using Dapper;
using Grpc.Core;
using MySql.Data.MySqlClient;
using MasterData.Workbackground.Protos;

namespace MasterData.Repositories.MySql
{
    public class WorkBackgroundDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public WorkBackgroundDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> SaveorUpdate(Models.WorkBackground o)
        { 
            var dt = DateTime.UtcNow;
            string findQuery = "SELECT name from mst_background where id = @id";
            var oData = await _connection.QuerySingleOrDefaultAsync<Models.WorkBackground>(findQuery, new { id = o.Id });

            string sqlQuery = @"INSERT into mst_background (id, name, is_active, index_cmb) VALUES 
                                (@id, @name, @description, @is_active, @index_cmb)";
            string updateQuery = @"UPDATE mst_background set name = @name, is_active = @is_active, 
                                index_cmb = @index_cmb where id = @id";
            int res = 0;
            if (oData == null)
            {
                res = await _connection.ExecuteAsync(sqlQuery, new
                {
                    id = o.Id,
                    name = o.Name,
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
                    is_active = true,
                    index_cmb = o.IndexCmb,
                });
            }

            return res > 0;
        }

        public async Task<Models.WorkBackground> GetById(string id)
        {
            try
            {
                string sqlQuery = "SELECT id, name from mst_background where id = @id and is_active = 1";
                var res = await _connection.QuerySingleOrDefaultAsync<Models.WorkBackground>(sqlQuery, new { id = id });
                return (Models.WorkBackground)res;

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Models.WorkBackground>> GetAll()
        {
            try
            {
                string sqlQuery = "SELECT id, name, is_active, index_cmb from mst_background where is_active = 1 order by index_cmb asc";
                var oData = await _connection.QueryAsync<Models.WorkBackground>(sqlQuery);
                return (List<Models.WorkBackground>)oData;
            }
            catch
            {
                throw;
            }
        }
    }

}
