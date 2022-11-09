using Dapper;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace MasterData.Repositories.MySql
{
    public class CompanyDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public CompanyDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<int> Create(Models.Company o)
        {
            string sqlQuery = @"INSERT into mt_company (id, id2, name, is_active)
                                    VALUES (@id, @id2, @name, @is_active)";
            var ret = await _connection.ExecuteAsync(sqlQuery, new { id = o.Id, id2 = o.Id2, name = o.Name, is_active = true });
            return ret;
        }

        public async Task<List<Models.Company>> GetAll()
        {
            string sqlQuery = @"SELECT id, id2, name from mt_company
                                where is_active = 1 order by id";

            var lComp = await _connection.QueryAsync<Models.Company>(sqlQuery);
            return (List<Models.Company>)lComp;
        }

        public async Task<Models.Company> GetById(string id)
        {
            try
            {
                string sqlQuery = @"SELECT id, id2, name, is_active from mt_company
                                where (id = @id or id2 = @id)";

                var oComp = await _connection.QueryFirstOrDefaultAsync<Models.Company>(sqlQuery, new { id = id, id2 = id });
                if (!oComp.IsActive)
                    throw new Exception("Company is not active, you cannot assign this company to pool");
                return oComp;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(Models.Company o)
        {
            string sqlQuery = @"UPDATE mt_company set name = @name, is_active = @is_active
                                where (id = @id or id2 = @id)";

            var ret = await _connection.ExecuteAsync(sqlQuery, new { name = o.Name, is_active = o.IsActive ? 1 : 0, id = o.Id });
            return ret;
        }
    }
}
