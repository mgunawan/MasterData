using MasterData.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Common.Logging;

namespace MasterData.Repositories.Cache
{
    public class RegionCache
    {
        private readonly IDistributedCache _redisCache;
        private readonly ILogger<RegionCache> _logger; 
        public RegionCache(IDistributedCache cache, ILogger<RegionCache> logger)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        const string baseRegionKey = "MD.BB1.region.";
        const string regionKey = "region.";
        const string provinceKey = "province.";
        const string provincesKey = "provinces.all";
        const string cityKey = "city.";
        const string citiesKey = "cities.";
        const string districtKey = "district.";
        const string districtsKey = "districts.";
        const string subDistrictKey = "subdistrict.";
        const string subDistrictsKey = "subdistricts.";

        public async Task<Models.Region> GetRegionById(int idRequest)
        {
            const string taskName = "GetRegionById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,regionKey,idRequest.ToString());

                SDLogging.Log($"Get data region id {idRequest} from redis.");
                var region = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(region)) 
                    return null;

                return JsonConvert.DeserializeObject<Models.Region>(region);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data region id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<Province> GetProvinceById(int idRequest)
        {
            const string taskName = "GetProvinceById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,provinceKey,idRequest.ToString());

                SDLogging.Log($"Get data province id {idRequest} from redis.");
                var province = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(province)) 
                    return null;

                return JsonConvert.DeserializeObject<Province>(province);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data province id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<Provinces> GetAllProvinces()
        {
            const string taskName = "GetAllProvinces";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,provincesKey);

                SDLogging.Log($"Get all data Provinces from redis.");
                var provinces = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(provinces)) 
                    return null;

                return JsonConvert.DeserializeObject<Provinces>(provinces);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get all data Provinces from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<City> GetCityById(int idRequest)
        {
            const string taskName = "GetCityById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,cityKey,idRequest.ToString());

                SDLogging.Log($"Get data city id {idRequest} from redis.");
                var city = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(city)) 
                    return null;

                return JsonConvert.DeserializeObject<City>(city);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data city id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<Cities> GetCitiesByProvinceId(int idRequest)
        {
            const string taskName = "GetCitiesByProvinceId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,citiesKey,idRequest.ToString());

                SDLogging.Log($"Get data cities by province id {idRequest} from redis.");
                var cities = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(cities)) 
                    return null;

                return JsonConvert.DeserializeObject<Cities>(cities);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data cities from province id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<District> GetDistrictById(int idRequest)
        {
            const string taskName = "GetDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,districtKey,idRequest.ToString());

                SDLogging.Log($"Get data district id {idRequest} from redis.");
                var district = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(district))
                    return null;

                return JsonConvert.DeserializeObject<District>(district);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data district id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<Districts> GetDistrictsByCityId(int idRequest)
        {
            const string taskName = "GetDistrictsByCityId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,districtsKey,idRequest.ToString());

                SDLogging.Log($"Get data districts by city id {idRequest} from redis.");
                var districts = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(districts))
                    return null;

                return JsonConvert.DeserializeObject<Districts>(districts);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data districts by city id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<SubDistrict> GetSubDistrictById(int idRequest)
        {
            const string taskName = "GetSubDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,subDistrictKey,idRequest.ToString());

                SDLogging.Log($"Get data sub-district id {idRequest} from redis.");
                var subDistrict = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(subDistrict))
                    return null;

                return JsonConvert.DeserializeObject<SubDistrict>(subDistrict);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data sub-district id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<SubDistricts> GetSubDistrictsByDistrictId(int idRequest)
        {
            const string taskName = "GetSubDistrictsByDistrictId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var keyRedis = string.Concat(baseRegionKey,subDistrictsKey,idRequest.ToString());

                SDLogging.Log($"Get data sub-districts by district id {idRequest} from redis.");
                var subDistricts = await _redisCache.GetStringAsync(keyRedis);

                if (String.IsNullOrEmpty(subDistricts))
                    return null;

                return JsonConvert.DeserializeObject<SubDistricts>(subDistricts);
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error get data sub-districts by district id {idRequest} from redis.\nError : {ex.Message}", SDLogging.ERROR);
                return null;
            }
        }

        public async Task<bool> SetRegionById(Models.Region region)
        {
            const string taskName = "SetRegionById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            
            if (region == null) return false;
            
            try
            {
                var keyRedis = string.Concat(baseRegionKey,regionKey,region.Id.ToString());

                SDLogging.Log($"Set data Region Id {region.Id} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(region));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data region id {region.Id} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetProvinceById(Province province)
        {
            const string taskName = "SetProvinceById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (province == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,provinceKey,province.Id.ToString());

                SDLogging.Log($"Set data Province Id {province.Id} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(province));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data Province Id {province.Id} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }            
        }

        public async Task<bool> SetAllProvinces(Provinces provinces)
        {
            const string taskName = "SetAllProvinces";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (provinces == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,provincesKey);

                SDLogging.Log($"Set data all Provinces to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(provinces));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data all Provinces to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }            
        }

        public async Task<bool> SetCityById(City city)
        {
            const string taskName = "SetCityById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (city == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,cityKey,city.Id.ToString());

                SDLogging.Log($"Set data City Id {city.Id} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(city));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data City Id {city.Id} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetCitiesByProvinceId(Cities cities)
        {
            const string taskName = "SetCitiesByProvinceId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (cities == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,citiesKey,cities.cities[0].ProvinceId.ToString());

                SDLogging.Log($"Set data Cities by Province Id {cities.cities[0].ProvinceId} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(cities));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data Cities by Province Id {cities.cities[0].ProvinceId} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetDistrictById(District district)
        {
            const string taskName = "SetDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (district == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,districtKey,district.Id.ToString());

                SDLogging.Log($"Set data District Id {district.Id} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(district));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data District Id {district.Id} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetDistrictsByCityId(Districts districts)
        {
            const string taskName = "SetDistrictsByCityId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (districts == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,districtsKey,districts.districts[0].CityId.ToString());

                SDLogging.Log($"Set data Districts by City Id {districts.districts[0].CityId} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(districts));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data District by City Id {districts.districts[0].CityId} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetSubDistrictById(SubDistrict subDistrict)
        {
            const string taskName = "SetSubDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (subDistrict == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,subDistrictKey,subDistrict.Id.ToString());

                SDLogging.Log($"Set data SubDistrict Id {subDistrict.Id} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(subDistrict));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data SubDistrict Id {subDistrict.Id} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }

        public async Task<bool> SetSubDistrictsByDistrictId(SubDistricts subDistricts)
        {
            const string taskName = "SetSubDistrictsByDistrictId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            if (subDistricts == null) return false;

            try
            {
                var keyRedis = string.Concat(baseRegionKey,subDistrictsKey,subDistricts.subDistricts[0].DistrictId.ToString());

                SDLogging.Log($"Set data SubDistrict by District Id {subDistricts.subDistricts[0].DistrictId} to redis.");
                await _redisCache.SetStringAsync(keyRedis, JsonConvert.SerializeObject(subDistricts));

                return true;
            }
            catch (Exception ex)
            {
                SDLogging.Log($"Error set data SubDistrict by District Id {subDistricts.subDistricts[0].DistrictId} to redis.\nError : {ex.Message}", SDLogging.ERROR);
                return false;
            }
        }
    }
}