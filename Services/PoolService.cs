using AutoMapper;
using Grpc.Core;
using MasterData.Pool.Protos;
using MasterData.Area.Protos;
using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;
using MasterData.Repositories.Cache;
using StatusPool = MasterData.Models.StatusPool;
using Common.Helpers;
using Google.Protobuf;
using System.Runtime.InteropServices;
using MasterData.Models.Dummy;

namespace MasterData.Services
{
    public class PoolService : PoolGrpcService.PoolGrpcServiceBase
    {
        private IPoolRepository repo;
        private ICompanyRepository repoCompany;
        private readonly IMapper mapper;
        private readonly ILogger<PoolService> log;

        public PoolService(IPoolRepository _repo, ICompanyRepository _repoCompany,
            ILogger<PoolService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            repoCompany = _repoCompany ?? throw new ArgumentNullException(nameof(_repoCompany));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<PoolEmpty> CreatePool(PoolModel request, ServerCallContext context)
        {
            log.LogInformation($"Begin grpc call PoolService.CreatePool for pool {request.Name}");
            try
            {
                var tgl = DateTime.Now;

                Models.Pool oPool = new Models.Pool
                {
                    Id = request.Id,
                    Id2 = request.Id2,
                    Name = request.Name,
                    Address = request.Address,
                    Telephone = request.Telephone,
                    IsActive = request.IsActive,
                    StatusPool = (StatusPool)request.StatusPool,
                    ActiveOn = request.ActiveOn.ToDate(),
                    NonActiveOn = request.ActiveOn.ToDate().AddYears(40),
                    ParentId = request.ParentId,
                    AreaId = request.AreaId,
                    CreateOn = tgl,
                    UpdateOn = tgl,
                    UserId = request.UserId, // context.GetHttpContext().User.Identity.Name,
                    ComputerName = request.ComputerName,
                };

                var res = await repo.db().Create(oPool);
                if (res)
                {
                    _ = repo.cache().SetCache(oPool);
                }
                log.LogInformation("INSERT data successful");
                return new PoolEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                log.LogInformation("FAILED insert data, error " + ex.Message);
                context.Status = new Status(StatusCode.Cancelled, $"Create pool with id {request.Id} failed.");
                return new PoolEmpty { Message = "FAILED" };
            }
        }

        public override async Task<PoolEmpty> UpdatePool(PoolModel request, ServerCallContext context)
        {
            try
            {
                log.LogInformation($"Begin grpc call PoolService.UpdatePool for pool {request.Name}");
                string id = request.Id2;
                var oPool = await repo.db().GetPoolById(id);
                if (oPool != null)
                {
                    oPool.Name = request.Name;
                    oPool.Address = request.Address;
                    oPool.IsActive = request.IsActive;
                    oPool.StatusPool = (StatusPool)request.StatusPool;
                    oPool.ActiveOn = request.ActiveOn.ToDate();
                    oPool.ParentId = request.ParentId;
                    oPool.Telephone = request.Telephone;
                    oPool.AreaId = request.AreaId;

                    await repo.db().Update(oPool);
                    await repo.cache().RemovePoolById(oPool.Id);
                }
                else
                {
                    throw new Exception("Pool not found.");
                }
                log.LogInformation("UPDATE data successfull.");
                return new PoolEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                log.LogError("UPDATE FAILED, " + ex.Message);
                context.Status = new Status(StatusCode.Cancelled, "Update data failed " + ex.Message);
                return new PoolEmpty { Message = "FAILED" };
            }

        }

        public async override Task<PoolEmpty> DeletePool(ReqDeletePoolById request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call PoolService.Delete");
                string id = request.Id;
                _ = await repo.db().Delete(id, false);
                await repo.cache().RemovePoolById(id);

                log.LogInformation("SUCCESS Delete data");
                return new PoolEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Cancelled, $"Delete data failed {ex.Message}");
                log.LogError("DELETE DATA FAIELD" + ex.Message);
                return new PoolEmpty { Message = "FAILED" };
            }

        }

        public override async Task<ResGetPoolById> GetPool(ReqGetPoolById request, ServerCallContext context)
        {
            try
            {
                log.LogInformation($"Begin call service Pool.GetPool {request.PoolId}");
                var oRes = new ResGetPoolById();
                oRes.Pool = new PoolModel();

                string id = request.PoolId;
                Models.Pool oPool = await repo.cache().GetPoolById(id);
                if (oPool == null)
                {
                    oPool = await repo.db().GetPoolById(id);
                    await repo.cache().SetCache(oPool);
                }

                if (oPool != null)
                {
                    oRes.Pool.Id = oPool.Id;
                    oRes.Pool.Id2 = oPool.Id2;
                    oRes.Pool.Name = oPool.Name;
                    oRes.Pool.Address = oPool.Address;
                    oRes.Pool.AreaId = oPool.AreaId;
                    oRes.Pool.Telephone = oPool.Telephone + "";
                    oRes.Pool.IsActive = oPool.IsActive;
                    oRes.Pool.StatusPool = (Pool.Protos.StatusPool)oPool.StatusPool;

                    if (oPool.Pools != null)
                    {
                        foreach (var item in oPool.Pools)
                        {
                            oRes.ListSatelit.Add(new PoolModel
                            {
                                Id = item.Id,
                                Id2 = item.Id2,
                                Name = item.Name,
                                Address = item.Address,
                                IsActive = item.IsActive,
                                StatusPool = (Pool.Protos.StatusPool)item.StatusPool,
                            });
                        }
                    }

                    if (oPool.Companies != null)
                    {
                        foreach (var item in oPool.Companies)
                        {
                            oRes.ListCompany.Add(new PoolCompanyModel
                            {
                                Id = item.CompanyId,
                                Name = item.Name,
                                ServiceType = item.ServiceType,
                                IsActive = item.IsActive,
                                //IsInduk = item.IsInduk,
                            });
                        }
                    }
                    var resData = mapper.Map<ResGetPoolById>(oRes);
                    return resData;
                }
                else
                {
                    throw new Exception("Data pool not found.");
                }
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.NotFound, $"Error for get data pool {ex.Message}");
                log.LogError("ERROR : " + ex.Message);
                return null;
            }
        }

        public override async Task<ResGetAllPool> GetAllPool(ReqCriteriaPool request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call service Pool:GetAllPool");
                var lRes = new ResGetAllPool();
                List<Models.Pool> ret = await repo.cache().GetPools();
                if (ret == null)
                {
                    ret = await repo.db().GetAllPool();
                    await repo.cache().SetPools(ret);
                }

                if (ret != null)
                {
                    foreach (var o in ret)
                    {
                        lRes.ListPool.Add(new PoolModel
                        {
                            Id = o.Id,
                            Id2 = o.Id2,
                            Name = o.Name,
                            Address = o.Address,
                            Telephone = o.Telephone,
                            AreaId = o.AreaId,
                            IsActive = o.IsActive,
                            StatusPool = (Pool.Protos.StatusPool)o.StatusPool,
                            ParentId = o.ParentId,
                            ActiveOn = o.ActiveOn.ToString("yyyy-MM-dd"),
                            NonActiveOn = o.NonActiveOn.ToString("yyyy-MM-dd"),
                            CreateOn = o.CreateOn.ToString("yyyy-MM-dd"),
                            UpdateOn = o.UpdateOn.ToString("yyyy-MM-dd"),
                            UserId = o.UserId,
                            ComputerName = o.ComputerName,
                        });
                    }
                }
                return lRes;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.NotFound, $"Error for request GetAllPool {ex.Message}");
                log.LogError("ERROR : " + ex.Message);
                return null;
            }
        }

        public override async Task<ResGetAllPool> GetPoolByAreaId(ReqPoolByAreaId request, ServerCallContext context)
        {
            log.LogInformation("Begin call service Pool:GetPoolByAreaId");
            var lRest = new ResGetAllPool();
            try
            {
                var areaId = request.AreaId;
                List<Models.Pool> ret = await repo.cache().GetPoolByAreaId(areaId);
                if (ret == null)
                {
                    ret = await repo.db().GetPoolByAreaId(areaId);
                    await repo.cache().SetPoolByAreaId(ret);
                }

                foreach (var o in ret)
                {
                    var oModel = Model2ProtoModel(o);
                    lRest.ListPool.Add(oModel);
                }
            }
            catch (Exception ex)
            {
                log.LogError("ERROR " + ex.Message);
            }
            return lRest;
        }

        private PoolModel Model2ProtoModel(Models.Pool o)
        {
            return new PoolModel
            {
                Id = o.Id,
                Id2 = o.Id2,
                Name = o.Name,
                Address = o.Address,
                Telephone = o.Telephone,
                AreaId = o.AreaId,
                IsActive = o.IsActive,
                StatusPool = (Pool.Protos.StatusPool)o.StatusPool,
                ParentId = o.ParentId,
                ActiveOn = o.ActiveOn.ToString("yyyy-MM-dd"),
                NonActiveOn = o.NonActiveOn.ToString("yyyy-MM-dd"),
                CreateOn = o.CreateOn.ToString("yyyy-MM-dd"),
                UpdateOn = o.UpdateOn.ToString("yyyy-MM-dd"),
                UserId = o.UserId,
                ComputerName = o.ComputerName,
            };
        }

        public override async Task<PoolEmpty> AddCompany(reqAddCompanyPool request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin Pool add company");
                var oPool = await repo.db().GetPoolById(request.PoolId);
                var lComp = new List<PoolCompany>();
                foreach (var item in request.Companies)
                {
                    var oComp = await repoCompany.db().GetById(item.Id);
                    if (oComp.Id != "")
                    {
                        lComp.Add(new Models.Dummy.PoolCompany
                        {
                            PoolId = item.Id,
                            Name = item.Name,
                            IsActive = item.IsActive,
                            ServiceType = item.ServiceType,
                            //IsInduk = item.IsInduk,
                            CreateOn = DateTime.Now,
                            UpdateOn = DateTime.Now,
                        });
                    }
                    else
                    {
                        log.LogError("ERROR: Company not found.");
                        throw new Exception("Company code not found.");
                    }
                }
                _ = await repo.db().AddCompanyPool(oPool, lComp);
                await repo.cache().DeleteCahce(oPool);
                return new PoolEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                log.LogError("ERROR : " + ex.Message);
                context.Status = new Status(StatusCode.Aborted, ex.Message);
                return new PoolEmpty { Message = "FAILED" };
            }
        }

        public override async Task<PoolEmpty> AddSatelit(reqAddSatelitPool request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Begin call service pool.addsatelit");
                var lSat = new List<string>();
                foreach (var item in request.SatelitPoolId)
                {
                    lSat.Add(item.ToString());
                }

                var lRes = await repo.db().AddSatelitPool(request.PoolId, lSat);
                return new PoolEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, $"Failed add satelit, error {ex.Message}");
                log.LogError("ERROR : " + ex.Message);
                return new PoolEmpty { Message = "FAILED" };
            }

        }

        public override async Task<ResAllPoolService> GetAllPoolService(ReqGetPoolById request, ServerCallContext context)
        {
            var oRes = new ResAllPoolService();
            try
            {
                log.LogInformation("Begin call service pool.getallpoolservice");
                var lPool = await repo.cache().GetPoolCompanyService(request.PoolId);
                if (lPool == null || lPool.Count == 0)
                {
                    lPool = (List<PoolCompany>)await repo.db().GetAllPoolService(request.PoolId);
                    if (lPool.Count > 0)
                        await repo.cache().SetPoolCompanyService(lPool);
                }

                foreach (var item in lPool)
                {
                    oRes.ListPoolService.Add(new Pool.Protos.PoolServiceType
                    {
                        PoolId = item.PoolId,
                        CompanyId = item.CompanyId,
                        Name = item.Name,
                        ServiceType = item.ServiceType,
                    });
                }
                return oRes;
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Aborted, $"Error getcompanyservice {ex.Message}");
                log.LogError("ERROR " + ex.Message);
                return null;
            }
        }
    }
}
