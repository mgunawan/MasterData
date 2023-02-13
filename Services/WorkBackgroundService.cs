using AutoMapper;
using Common.Logging;
using Grpc.Core;
using MasterData.Area.Protos;
using MasterData.Media.Protos;
using MasterData.Repositories.Interface;
using Workbackground.Protos;

namespace MasterData.Services
{
    public class WorkBackgroundService: WorkBackgroundGrpcService.WorkBackgroundGrpcServiceBase
    {
        private IWorkBackgroundRepository repo;
        private readonly IMapper mapper;
        private readonly ILogger<WorkBackgroundService> log;

        public WorkBackgroundService(IWorkBackgroundRepository _repo,
            ILogger<WorkBackgroundService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<BackgroundEmpty> SaveOrUpdate(WorkBackgroundModel request, ServerCallContext context)
        {
            try
            {
                SDLogging.Log($"Begin call service WorkBackground : {request.Name}");
                var o = mapper.Map<Models.WorkBackground>(request);
                var res = await repo.db().SaveorUpdate(o);
                if (res)
                {
                    _ = repo.cache().SetCache(o);
                    _ = repo.cache().DeleteCache(); //delete old list area cache
                }
                return new BackgroundEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, "Failed insert/update new work background, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message, SDLogging.ERROR);
                throw;
            }
        }

        public override async Task<WorkBackgroundModel> GetById(reqBackgroundId request, ServerCallContext context)
        {
            WorkBackgroundModel o = new WorkBackgroundModel();
            try
            {
                SDLogging.Log($"Begin call service get work background id: {request.Id}");
                Models.WorkBackground ret = await repo.cache().GetById(request.Id);
                if (ret == null)
                {
                    ret = await repo.db().GetById(request.Id);
                    if (ret != null)
                    {
                        _ = await repo.cache().SetCache(ret);
                    }
                    else
                    {
                        throw new Exception("Work background for this id not found");
                    }
                }

                o = mapper.Map<WorkBackgroundModel>(ret);
                return o;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.NotFound, "Failed get work background, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message, SDLogging.ERROR);
                return o;
            }
        }

        public override async Task<resBackgroundAll> GetAll(BackgroundEmpty request, ServerCallContext context)
        {
            log.LogInformation("Call Media Service GetAll");
            try
            {
                var rs = new resBackgroundAll();
                List<Models.WorkBackground> res = await repo.cache().GetAll();

                if (res == null)
                    res = await repo.db().GetAll();

                foreach (var item in res)
                {
                    rs.ListBackground.Add(new WorkBackgroundModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IndexCmb = item.IndexCmb,
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
    }
}
