using MySql.Data.MySqlClient;
using Dapper;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using MasterData.Models;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using MasterData.Models.Dummy;

namespace MasterData.Repositories.MySql
{
    public class PoolDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public PoolDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
        public async Task<bool> Create(Models.Pool o)
        {
            try
            {
                var sqlArea = "SELECT id, name from mst_area where id = @id";
                var oArea = await _connection.QuerySingleOrDefaultAsync<Models.Area>(sqlArea, new { id = o.AreaId });
                if (oArea == null)
                    throw new Exception("Area id not found.");

                var sqlQuery = "INSERT into mst_pool (" +
                    "id, " +
                    "id2, " +
                    "name, " +
                    "address, " +
                    "telephone, " +
                    "area_id, " +
                    "is_active, " +
                    "status_pool, " +
                    "parent_id, " +
                    "active_on, " +
                    "non_active_on, " +
                    "create_on, " +
                    "update_on, " +
                    "user_id, " +
                    "computer_name) VALUES (" +
                    "@id, " +
                    "@id2, " +
                    "@name," +
                    "@address," +
                    "@telephone," +
                    "@areaid," +
                    "@isactive," +
                    "@statuspool," +
                    "@parentid," +
                    "@activeon," +
                    "@nonactiveon," +
                    "@createon," +
                    "@updateon," +
                    "@userid," +
                    "@computername)";

                var res = await _connection.ExecuteAsync(sqlQuery, new
                {
                    id = o.Id,
                    id2 = o.Id2,
                    name = o.Name,
                    address = o.Address,
                    telephone = o.Telephone,
                    areaid = o.AreaId,
                    isactive = o.IsActive,
                    statuspool = o.StatusPool,
                    parentid = o.ParentId,
                    activeon = o.ActiveOn,
                    nonactiveon = o.NonActiveOn,
                    createon = o.CreateOn,
                    updateon = o.UpdateOn,
                    userid = o.UserId,
                    computername = o.ComputerName
                });
                return res > 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Models.Pool> GetPoolById(string id)
        {
            string sqlQuery = @"Select id, id2, name, address, telephone, " +
                "area_id, is_active, status_pool,parent_id, active_on, " +
                "non_active_on, create_on, update_on, user_id, computer_name " +
                "FROM mst_pool where (id = @id or id2 = @id)";

            var res = await _connection.QueryFirstOrDefaultAsync<Models.Pool>(sqlQuery, new { id = id });
            if (res != null)
            {
                var lSat = GetSatelitByPoolId(id);
                if (lSat != null)
                {
                    res.Pools = new List<Models.Pool>();
                    foreach (var item in await lSat)
                    {
                        res.Pools.Add(new Models.Pool
                        {
                            Id = item.Id,
                            Id2 = item.Id2,
                            Name = item.Name,
                            Address = item.Address,
                            IsActive = item.IsActive,
                            StatusPool = item.StatusPool,
                        });
                    }
                }

                var lComp = GetCompanyByPoolId(id);
                if (lComp != null)
                {
                    res.Companies = new List<PoolCompany>();
                    foreach (var item in await lComp)
                    {
                        res.Companies.Add(new Models.Dummy.PoolCompany
                        {
                            CompanyId = item.CompanyId,
                            IsActive = item.IsActive,
                            ServiceType = item.ServiceType,
                            //IsInduk = item.IsInduk,
                            Name = item.Name,
                        });
                    }
                }
            }
            return res;
        }

        public Task<IEnumerable<PoolCompany>> GetCompanyByPoolId(string id)
        {
            string sqlQuery = @"Select a.pool_id, a.company_id, b.name, a.is_induk, a.service_type, a.is_active
                        FROM mst_pool_company a 
                        LEFT join mst_company b on b.id = a.company_id where a.pool_id = @id";

            var res = _connection.QueryAsync<PoolCompany>(sqlQuery, new { id = id });
            return res;
        }

        public Task<IEnumerable<Models.Pool>> GetSatelitByPoolId(string parentId)
        {
            string sqlQuery = @"Select id, id2, name, address, telephone, " +
               "area_id, is_active, status_pool, parent_id, active_on, " +
               "non_active_on, create_on, update_on, user_id, computer_name " +
               "FROM mst_pool where parent_id = @parentid";

            var res = _connection.QueryAsync<Models.Pool>(sqlQuery, new { parentid = parentId });
            return res;
        }

        public async Task<bool> Delete(string poolId, bool status)
        {
            string sqlQuery = "Update mst_pool set is_active = 0 where id2 = @id";
            try
            {
                var res = await _connection.ExecuteAsync(sqlQuery, new { isactive = status, id = poolId });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Models.Pool o)
        {
            try
            {
                var oArea = await (new AreaDb(_configuration).GetById(o.AreaId));
                if (oArea == null)
                    throw new Exception("Area not found.");

                string sqlQuery = @"UPDATE mst_pool set name=@name, address=@address, telephone=@telephone,
                        area_id=@area_id, is_active=@is_active, status_pool=@status_pool, parent_id=@parent_id, active_on=@active_on,
                        non_active_on=@non_active_on, update_on=@update_on, user_id=@user_id, computer_name=@computer_name where (id=@id and id2=@id2)";

                if (o.IsActive)
                    o.NonActiveOn = DateTime.Now.AddYears(40);

                var ret = await _connection.ExecuteAsync(sqlQuery, new
                {
                    id = o.Id,
                    id2 = o.Id2,
                    name = o.Name,
                    address = o.Address,
                    telephone = o.Telephone,
                    area_id = o.AreaId,
                    is_active = o.IsActive,
                    status_pool = o.StatusPool,
                    parent_id = o.ParentId,
                    active_on = o.ActiveOn,
                    non_active_on = o.NonActiveOn,
                    update_on = DateTime.Now,
                    user_id = o.UserId,
                    computer_name = o.ComputerName
                });
                return ret > 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AddCompanyPool(Models.Pool o, List<PoolCompany> lComp)
        {
            try
            {
                string sqlQuery;
                var oPool = await GetPoolById(o.Id);
                if (oPool != null)
                {
                    foreach (var item in lComp)
                    {
                        sqlQuery = $"INSERT into mst_pool_company (pool_id, company_id, is_induk, service_type, is_active, create_on, update_on) VALUES (@poolid, @companyid, @isInduk, @serviceType, @isactive, @createon, @updateon)";
                        var rec = _connection.ExecuteAsync(sqlQuery, new { poolid = oPool.Id2, companyid = item.PoolId, isinduk = false, servicetype = item.ServiceType, isactive = item.IsActive, createon = item.CreateOn, item.UpdateOn });
                        //var rec = _connection.ExecuteAsync(sqlQuery, new { poolid = oPool.Id2, companyid = item.PoolId, isinduk = item.IsInduk, servicetype = item.ServiceType, isactive = item.IsActive, createon = item.CreateOn, item.UpdateOn });
                    }
                    return true;
                }
                else
                {
                    throw new Exception("Pool not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddSatelitPool(string poolId, List<string> lSat)
        {
            try
            {
                string sqlQuery1 = "UPDATE mst_pool set parent_id = null, status_pool = @statuspool where id2 = @poolid";
                var ret = await _connection.ExecuteAsync(sqlQuery1, new { statuspool = (int)StatusPool.PARENT, poolid = poolId });
                if (ret > 0)
                {
                    string sqlQuery2 = "UPDATE mst_pool set parent_id = @parentid, status_pool = @statuspool where id2 = @poolid";
                    int counter = 0;
                    foreach (var item in lSat)
                    {
                        var res = _connection.ExecuteAsync(sqlQuery2, new { parentid = poolId, statuspool = (int)StatusPool.SATELITE, poolid = item });
                        counter++;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Models.Pool>> GetAllPool()
        {
            try
            {
                string sqlQuery = @"SELECT id, id2, name, address, telephone, area_id,
                                    is_active, status_pool, parent_id, active_on, non_active_on,
                                    create_on, update_on, user_id, computer_name
                                    FROM mst_pool order by id";
                var ret = await _connection.QueryAsync<Models.Pool>(sqlQuery);
                return (List<Models.Pool>)ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Models.Pool>> GetPoolByAreaId(string regId)
        {
            try
            {
                string sqlQuery = @"SELECT id, id2, name, address, telephone, area_id,
                                    is_active, status_pool, parent_id, active_on, non_active_on,
                                    create_on, update_on, user_id, computer_name
                                    FROM mst_pool where area_id = @areaid order by id";
                var ret = await _connection.QueryAsync<Models.Pool>(sqlQuery, new { areaid = regId });
                return (List<Models.Pool>)ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PoolCompany>> GetAllPoolService(string poolId)
        {
            try
            {
                string sqlQuery = @"Select a.pool_id, a.company_id, c.name, a.service_type from mst_pool_company a
                    left join mst_pool b on b.id2 = a.pool_id 
                    left join mst_company c on c.id = a.company_id
                    where a.is_active = 1 and a.pool_id = @poolid";
                var ret = await _connection.QueryAsync<PoolCompany>(sqlQuery, new {poolid = poolId});
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}
