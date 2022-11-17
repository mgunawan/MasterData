using AutoMapper;
using Common.Enums;
using Grpc.Core;
using MasterData.Repositories.Interface;
using MasterData.VM.Protos;
using Serilog;

namespace MasterData.Services
{
    public class VehicleTypeService : ServiceTypeGrpcService.ServiceTypeGrpcServiceBase
    {
        private readonly IServiceTypeRepository repo;
        private readonly IMapper mapper;
        private readonly ILogger<VehicleTypeService> log;

        public VehicleTypeService(IServiceTypeRepository _repo, ILogger<VehicleTypeService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<ServiceTypeEmpty> Create(ServiceTypeModel request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call service Vehicle Type.");
                Models.VM.ServiceType oService = mapper.Map<Models.VM.ServiceType>(request);
                var ret = await repo.db().Create(oService);
                if(ret > 0)
                {
                    await repo.cache().DeleteCache();
                    await repo.cache().SetCache(oService);
                    return new ServiceTypeEmpty { Message = "OK" };
                }
                return new ServiceTypeEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Cancelled, "Error on create seervice type");
                log.LogError("Error on create service type " + ex.Message);
                return new ServiceTypeEmpty { Message = "FAILED" };
            }
        }

        public override async Task<ServiceTypeEmpty> Update(ServiceTypeModel request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call service Vehicle Type update.");
                Models.VM.ServiceType oService = mapper.Map<Models.VM.ServiceType>(request);
                var ret = await repo.db().Update(oService);
                if (ret > 0)
                {
                    await repo.cache().DeleteCache();
                    await repo.cache().SetCache(oService);
                    return new ServiceTypeEmpty { Message = "OK" };
                }
                return new ServiceTypeEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Cancelled, "Error on create seervice type");
                log.LogError("Error on create service type " + ex.Message);
                return new ServiceTypeEmpty { Message = "FAILED" };
            }
        }

        public override async Task<ServiceTypeModel> GetById(reqGetByPoolAndId request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call MasterData.VehicleServiceType:GetById");
                var pool_id = request.PoolId;
                var service_id = request.ServiceTypeId;
                Models.VM.ServiceType oService = await repo.cache().GetByPoolAndId(pool_id, service_id);
                if(oService == null)
                {
                    oService = await repo.db().GetByPoolAndId(pool_id, service_id);
                    if (oService != null)
                        await repo.cache().SetCachePoolAndId(pool_id, oService);
                    else
                        throw new Exception("Service id for this pool, not available.");
                }
                var oRet = mapper.Map<ServiceTypeModel>(oService);
                return oRet;
            }
            catch(Exception ex)
            {
                log.LogError($"Error when call MasterData.VehicleServiceType:GetById {ex.Message}");
                context.Status = new Status(StatusCode.NotFound, $"Error when call MasterData.VehicleServiceType:GetById {ex.Message}");
                return default(ServiceTypeModel);
            }
        }

        public override async Task<resGetAll> GetListService(reqGetByPool request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call service VehicleTypeService.GetListService.");
                var ret = new resGetAll();
                var listServiceType = await repo.cache().GetListCache();
                if (listServiceType == null)
                {
                    listServiceType = await repo.db().GetAll(request.PoolId);
                    await repo.cache().SetListCache(listServiceType);
                }

                foreach (var item in listServiceType)
                {
                    ret.ListService.Add(new ServiceTypeModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        AvailableFor = item.AvailableFor,
                    });
                }
                return ret;
            }
            catch(Exception ex)
            {
                log.LogError($"ERROR when call VehicleTypeService.GetListService {ex.Message}");
                context.Status = new Status(StatusCode.NotFound, $"Error on call VehicleTypeService.GetListService {ex.Message}");
                return default(resGetAll);
            }
        }
    }
}
