using MasterData.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using MasterData.Repositories.Interface;
using Common.Logging;

namespace MasterData.Repositories.MySql
{
    public class RegionDb
    {
        private readonly IConfiguration _configuration;
        private MySqlConnection _connection;
        private readonly ILogger<RegionDb> _logger;

        public RegionDb(IConfiguration configuration, ILogger<RegionDb> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new MySqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")) ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger;
        }

        public async Task<Models.Region> GetRegionById(int idRequest)
        {
            const string taskName = "GetRegionById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data region id {idRequest} from DB.");
                var region = await _connection.QueryFirstOrDefaultAsync<Models.Region>
                (RegionQuery.GetRegionById, new { Id = idRequest });

                return region;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data region id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<Province> GetProvinceById(int idRequest)
        {
            const string taskName = "GetProvinceById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data province id {idRequest} from DB.");
                var province = await _connection.QueryFirstOrDefaultAsync<Province>
                (RegionQuery.GetProvinceById, new { Id = idRequest });

                return province;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data province id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
               throw;
            } 
        }

        public async Task<Provinces> GetAllProvinces()
        {
            const string taskName = "GetAllProvinces";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get all data provinces from DB.");
                var provinces = await _connection.QueryAsync<Province>(RegionQuery.GetProvince);

                if (provinces.Count() == 0)
                    return null;

                var objProvince = new Provinces() {provinces = provinces.ToList()};
                return objProvince;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get all data province from DB.\nError : {ex.Message}", SDLogging.ERROR);
               throw;
            } 
        }

        public async Task<City> GetCityById(int idRequest)
        {
            const string taskName = "GetCityById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data city id {idRequest} from DB.");
                var city = await _connection.QueryFirstOrDefaultAsync<City>
                (RegionQuery.GetCityById, new { Id = idRequest });

                return city;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data city id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<Cities> GetCitiesByProvinceId(int idRequest)
        {
            const string taskName = "GetCitiesByProvinceId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data cities by province id {idRequest} from DB.");
                var cities = await _connection.QueryAsync<City>(RegionQuery.GetCitiesByProvinceId, new {Id = idRequest});

                if (cities.Count() == 0) 
                    return null;

                var objCity = new Cities() {cities = cities.ToList()};
                return objCity;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data cities by province id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<District> GetDistrictById(int idRequest)
        {
            const string taskName = "GetDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data district id {idRequest} from DB.");
                var district = await _connection.QueryFirstOrDefaultAsync<District>
                (RegionQuery.GetDistrictById, new { Id = idRequest });

                return district;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data district id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<Districts> GetDistrictsByCityId(int idRequest)
        {
            const string taskName = "GetDistrictsByCityId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data districts by city id {idRequest} from DB.");
                var districts = await _connection.QueryAsync<District>(RegionQuery.GetDistrictsByCityId, new {Id = idRequest});

                if (districts.Count() == 0)
                    return null;

                var objDistrict = new Districts(){districts = districts.ToList()};
                return objDistrict;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data districts by city Id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<SubDistrict> GetSubDistrictById(int idRequest)
        {
            const string taskName = "GetSubDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data sub-district id {idRequest} from DB.");
                var subDistrict = await _connection.QueryFirstOrDefaultAsync<SubDistrict>
                (RegionQuery.GetSubDistrictById, new { Id = idRequest });

                return subDistrict;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data sub-district id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }

        public async Task<SubDistricts> GetSubDistrictsByDistrictId(int idRequest)
        {
            const string taskName = "GetSubDistrictsByDistrictId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                SDLogging.Log($"Get data sub-districts by district id {idRequest} from DB.");
                var subDistricts = await _connection.QueryAsync<SubDistrict>(RegionQuery.GetSubDistrictsByDistrictId, new {Id = idRequest});

                if (subDistricts.Count() == 0)
                    return null;
                    
                var objSubDistricts = new SubDistricts() {subDistricts = subDistricts.ToList()};
                return objSubDistricts;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data sub-districts by district id {idRequest} from DB.\nError : {ex.Message}", SDLogging.ERROR);
                throw;
            }
        }
    }
}