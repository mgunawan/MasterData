﻿using Dapper;
using MasterData.Models.VM;
using MySql.Data.MySqlClient;

namespace MasterData.Repositories.MySql
{
    public class ServiceTypeDb
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public ServiceTypeDb(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<int> Create(Models.VM.ServiceType o)
        {
            try
            {
                string sqlQuery = @"INSERT into mt_vm_servicetype (id, name, available_for) VALUES (@id, @name, @available_for)";
                var ret = await _connection.ExecuteAsync(sqlQuery, new { id = o.Id, name = o.Name, available_for = o.AvailableFor });
                return ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(Models.VM.ServiceType o)
        {
            try
            {
                string sqlQuery = @"UPDATE mt_vm_servicetype set name = @name, available_for = @available_for where id = @id";
                var ret = await _connection.ExecuteAsync(sqlQuery, new { id = o.Id, name = o.Name, available_for = o.AvailableFor });
                return ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Models.VM.ServiceType>> GetAll(string poolId)
        {
            try
            {
                string sqlQuery = @"SELECT id, name, available_for from mt_vm_servicetype where available_for like @me";
                var lService = await _connection.QueryAsync<Models.VM.ServiceType>(sqlQuery, new { me = $"%{poolId}%" });
                return (List<Models.VM.ServiceType>)lService;
            }
            catch
            {
                throw;
            }
        }

        internal Task<ServiceType> GetByPoolAndId(string pool_id, string service_id)
        {
            throw new NotImplementedException();
        }
    }
}
