using AutoMapper;
using Common.Logging;
using Grpc.Core;
using MasterData.Area.Protos;
using MasterData.Media.Protos;
using MasterData.Repositories.Interface;

namespace MasterData.Services
{
    public class MediaService : MediaGrpcService.MediaGrpcServiceBase
    {
        private IMediaRepository repo;
        private readonly IMapper mapper;
        private readonly ILogger<MediaService> log;

        public MediaService(IMediaRepository _repo,
            ILogger<MediaService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<MediaEmpty> SaveOrUpdate(MediaModel request, ServerCallContext context)
        {
            try
            {
                SDLogging.Log($"Begin call service SaveOrUpdate: {request.Name}");
                //MApping manual
                var oMedia = mapper.Map<Models.Media>(request);
                var res = await repo.db().Create(oMedia);
                if (res)
                {
                    _ = repo.cache().SetCache(oMedia);
                }
                return new MediaEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, "Failed insert new media, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message, SDLogging.ERROR);
                throw;
            }
        }

        public override async Task<ResMedia> GetById(ReqMediaId request, ServerCallContext context)
        {
            ResMedia rm = new ResMedia();
            try
            {
                SDLogging.Log($"Begin call service get media id: {request.Id}");
                Models.Media ret = await repo.cache().GetById(request.Id);
                if (ret == null)
                {
                    ret = await repo.db().GetById(request.Id);
                    if (ret != null)
                    {
                        _ = await repo.cache().SetCache(ret);
                    }
                    else
                    {
                        throw new Exception("Media for this id not found");
                    }
                }

                var o = mapper.Map<MediaModel>(ret);
                rm.Media = o;
                return rm;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.NotFound, "Failed get media, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message, SDLogging.ERROR);
                return rm;
            }
        }

        public override async Task<ResMediaAll> GetAll(MediaEmpty request, ServerCallContext context)
        {
            try
            {
                ResMediaAll oResp = new ResMediaAll();
                SDLogging.Log($"Begin call service get all media");
                List<Models.Media> ret = await repo.cache().GetAll();
                if (ret == null)
                {
                    ret = await repo.db().GetAll();
                    if (ret != null)
                    {
                        _ = await repo.cache().SetCache(ret);
                    }
                    else
                    {
                        throw new Exception("Media not found");
                    }
                }

                foreach (var item in ret)
                {
                    oResp.ListMedia.Add(new MediaModel
                    {
                        Id = item.Id,
                        Name=item.Name,
                        Description=item.Description,
                        IsActive = item.IsActiive,
                        Index = item.IndexCmb
                    });
                }
                return oResp;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.NotFound, "Failed get media, error " + ex.Message);
                log.LogError(ex.Message);
                SDLogging.Log(ex.Message, SDLogging.ERROR);
                return null;
            }
        }
    }
}
