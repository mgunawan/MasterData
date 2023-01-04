using AutoMapper;
using MasterData.Models;
using MasterData.Region.Protos;
using MasterData.Repositories.Interface;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Common.Logging;

namespace MasterData.Services
{
    public class RegionService : RegionGrpcService.RegionGrpcServiceBase
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionService> _logger;

        public RegionService(IRegionRepository repository, IMapper mapper, ILogger<RegionService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<RegionGrpc> GetRegionById(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetRegionById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var Region = await _repository.cache().GetRegionById(request.Id);
                if (Region == null)
                {
                    Region = await _repository.db().GetRegionById(request.Id);
                    if (Region != null)
                        _ = await _repository.cache().SetRegionById(Region);
                }

                if (Region == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Region with Id {request.Id} is not found.");

                return _mapper.Map<RegionGrpc>(Region);    
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ProvinceGrpc> GetProvinceById(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetProvinceById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var province = await _repository.cache().GetProvinceById(request.Id);
                if (province == null)
                {
                    province = await _repository.db().GetProvinceById(request.Id);
                    if (province != null)
                        _ = await _repository.cache().SetProvinceById(province);
                }

                if (province == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Province with Id {request.Id} is not found.");
                
                return _mapper.Map<ProvinceGrpc>(province);
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ProvincesGrpc> GetAllProvinces(EmptyRequest emptyRequest, ServerCallContext context)
        {
            const string taskName = "GetAllProvinces";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");
            try
            {
                var provinces = await _repository.cache().GetAllProvinces();
                if (provinces == null)
                {
                    provinces = await _repository.db().GetAllProvinces();
                    if (provinces != null)
                        _ = await _repository.cache().SetAllProvinces(provinces);
                }

                if (provinces == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Data all provinces is not found.");

                return _mapper.Map<ProvincesGrpc>(provinces);
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<CityGrpc> GetCityById(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetCityById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var city = await _repository.cache().GetCityById(request.Id);
                if (city == null)
                {
                    city = await _repository.db().GetCityById(request.Id);
                    if (city != null)
                        _ = await _repository.cache().SetCityById(city);
                }

                if (city == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"City with Id {request.Id} is not found.");

                return _mapper.Map<CityGrpc>(city);    
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }            
        }

        public override async Task<CitiesGrpc> GetCitiesByProvinceId(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetCitiesByProvinceId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var cities = await _repository.cache().GetCitiesByProvinceId(request.Id);
                if (cities == null)
                {
                    cities = await _repository.db().GetCitiesByProvinceId(request.Id);
                    if (cities != null)
                        _ = await _repository.cache().SetCitiesByProvinceId(cities);
                }

                if (cities == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Cities with Province Id {request.Id} is not found.");

                return _mapper.Map<CitiesGrpc>(cities);    
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }            
        }

        public override async Task<DistrictGrpc> GetDistrictById(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var district = await _repository.cache().GetDistrictById(request.Id);
                if (district == null)
                {
                    district = await _repository.db().GetDistrictById(request.Id);
                    if (district != null)
                        _ = await _repository.cache().SetDistrictById(district);
                }

                if (district == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"District with Id {request.Id} is not found.");

                return _mapper.Map<DistrictGrpc>(district);   
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<DistrictsGrpc> GetDistrictsByCityId(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetDistrictsByCityId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var districts = await _repository.cache().GetDistrictsByCityId(request.Id);
                if (districts == null)
                {
                    districts = await _repository.db().GetDistrictsByCityId(request.Id);
                    if (districts != null)
                        _ = await _repository.cache().SetDistrictsByCityId(districts);
                }

                if (districts == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Districts with City Id {request.Id} is not found.");

                return _mapper.Map<DistrictsGrpc>(districts);   
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<SubDistrictGrpc> GetSubDistrictById(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetSubDistrictById";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var subDistrict = await _repository.cache().GetSubDistrictById(request.Id);
                if (subDistrict == null)
                {
                    subDistrict = await _repository.db().GetSubDistrictById(request.Id);
                    if (subDistrict != null)
                        _ = await _repository.cache().SetSubDistrictById(subDistrict);
                }

                if (subDistrict == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Sub District with Id {request.Id} is not found.");

                return _mapper.Map<SubDistrictGrpc>(subDistrict);    
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }    
        }

        public override async Task<SubDistrictsGrpc> GetSubDistrictsByDistrictId(IdRequest request, ServerCallContext context)
        {
            const string taskName = "GetSubDistrictsByDistrictId";
            SDLogging.Log($"Executing task {this.ToString()}.{taskName}");

            try
            {
                var subDistricts = await _repository.cache().GetSubDistrictsByDistrictId(request.Id);
                if (subDistricts == null)
                {
                    subDistricts = await _repository.db().GetSubDistrictsByDistrictId(request.Id);
                    if (subDistricts != null)
                        _ = await _repository.cache().SetSubDistrictsByDistrictId(subDistricts);
                }

                if (subDistricts == null)
                    context.Status = new Status(StatusCode.NotFound,
                        $"Sub District with District Id {request.Id} is not found.");

                return _mapper.Map<SubDistrictsGrpc>(subDistricts);    
            }
            catch (System.Exception ex)
            {
                SDLogging.Log($"Error executing task {this.ToString()}.{taskName}. Error : {ex.Message}", SDLogging.ERROR);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }    
        }
    }
}