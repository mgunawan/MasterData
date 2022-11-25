using AutoMapper;
using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;
using MasterData.Pool.Protos;
using MasterData.Area.Protos;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Authorization;
using Common.Logging;

namespace MasterData.Services
{
    public class AreaService : AreaGrpcService.AreaGrpcServiceBase
    {
        private IAreaRepository repo;
        private readonly IMapper mapper;
        private readonly ILogger<AreaService> log;

        public AreaService(IAreaRepository _repo,
            ILogger<AreaService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<AreaEmpty> AddArea(AreaModel request, ServerCallContext context)
        {
            try
            {
                SDLogging.Log($"Begin call service AddArea: {request.Name}");
                var oArea = mapper.Map<Models.Area>(request);
                var res = await repo.db().Create(oArea);
                if (res)
                {
                    _ = repo.cache().SetCache(oArea);
                    _ = repo.cache().DeleteCache(); //delete old list area cache
                }
                return new AreaEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, "Failed insert new area, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message,SDLogging.ERROR);
                throw;
            }
        }

        public override async Task<AreaModel> GetAreaById(reqAreaById request, ServerCallContext context)
        {
            try
            {
                SDLogging.Log($"Begin call service GetAreaById: {request.Id}");
                Models.Area ret = await repo.cache().GetById(request.Id);
                if (ret == null)
                {
                    ret = await repo.db().GetById(request.Id);
                    if (ret != null)
                    {
                        _ = await repo.cache().SetCache(ret);
                    }
                    else
                    {
                        throw new Exception("Area for this id not found");
                    }
                }

                var oArea = mapper.Map<AreaModel>(ret);
                return oArea;
            }
            catch(Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, "Failed find area, error " + ex.Message);
                SDLogging.Log(ex.Message,SDLogging.ERROR);
                throw;
            }
        }

        //[Authorize]
        public override async Task<resAreaAll> GetAllArea(AreaEmpty request, ServerCallContext context)
        {
            log.LogInformation("Call Area Service GetAll");
            try
            {
                var rs = new resAreaAll();
                List<Models.Area> res = await repo.cache().GetAll();
                
                if (res == null)
                    res = await repo.db().GetAll();
                    
                foreach (var item in res)
                {
                    rs.ListArea.Add(new AreaModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                _ = repo.cache().SetCache(res);
                return rs;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, "Getall data error");
                log.LogError(ex.Message);
                throw;
            }
        }

        //Set & GetCache
        //public async Task<T> GetInsights<T>(string cacheKey, IDistributedCache _distributedCache, int saveDurationInHour)
        //{
        //    var encodedResult = await _distributedCache.GetStringAsync(cacheKey);

        //    if (!string.IsNullOrWhiteSpace(encodedResult))
        //    {
        //        var cacheValue = JsonConvert.DeserializeObject<T>(encodedResult);
        //        return cacheValue;
        //    }

        //    var result = await GetResults<T>(); //Call to resource access
        //    encodedResult = JsonConvert.SerializeObject(result);
        //    await _distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(encodedResult), new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(saveDurationInHour)
        //    });

        //    return (T)result;
        //}

        //private async Task<T> GetResults<T>() where T : new()
        //{
        //    return await repo.db().GetAll();
        //}
    }
}
