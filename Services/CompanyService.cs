using AutoMapper;
using Grpc.Core;
using MasterData.Company.Protos;
using MasterData.Repositories.Cache;
using MasterData.Repositories.Interface;
using MasterData.Repositories.MySql;
using MasterData.Services;
using Serilog;
using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace MasterData.Services
{
    public class CompanyService : CompanyGrpcService.CompanyGrpcServiceBase
    {
        private readonly ICompanyRepository repo;
        private readonly IMapper mapper;
        private readonly ILogger<CompanyService> log;

        public CompanyService(ICompanyRepository _repo, ILogger<CompanyService> _log, IMapper _mapper)
        {
            repo = _repo ?? throw new ArgumentNullException(nameof(_repo));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
        }

        public override async Task<CompanyEmpty> Create(CompanyModel request, ServerCallContext context)
        {
            try
            {
                log.LogInformation($"Begin grpc call CompanyService.Create for {request.Id}, {request.Name}");
                var oComp = mapper.Map<Models.Company>(request);
                var ret = await repo.db()
                    .Create(oComp);
                if (ret > 0)
                {
                    context.Status = new Status(StatusCode.OK, "Successfull create data company");
                    return new CompanyEmpty { Message = "OK" };
                }
                return new CompanyEmpty { Message = "FAILED" };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.Cancelled, "Error create company " + ex.Message);
                log.LogError("Error when create Company.Create" + ex.Message);
                return new CompanyEmpty { Message = "FAILED" };
            }
        }

        public async override Task<resCompanyAll> GetAll(reqGetAll request, ServerCallContext context)
        {
            try
            {
                int rowPage = request.RowPage;
                int page = request.Page;
                string search = "";
                bool isDescending = request.IsDescending;

                log.LogInformation($"Begin grpc call CompanyService.GetAll");
                var lRet = new resCompanyAll();

                List<Models.Company> ret = new List<Models.Company>();
                ret = await repo.cache().GetCache();
                if (ret == null)
                    ret = await repo.db().GetAll(rowPage, page, search, isDescending);

                foreach (var item in ret)
                {
                    item.IsActive = true;
                    var oComp = mapper.Map<CompanyModel>(item);
                    lRet.ListCompany.Add(oComp);
                }
                return lRet;
            }
            catch (Exception ex)
            {
                log.LogError("Error call CompanyService.GetAll " + ex.Message);
                context.Status = new Status(StatusCode.NotFound, "Error create company " + ex.Message);
                return null;

            }
        }

        public override async Task<resCompanyAll> GetByCriteria(reqGetAll request, ServerCallContext context)
        {
            try
            {
                int rowPage = request.RowPage;
                int page = request.Page;
                string search = request.Search;
                bool isDescending = request.IsDescending;

                log.LogInformation($"Begin grpc call CompanyService.GetAll");
                var lRet = new resCompanyAll();

                List<Models.Company> ret = new List<Models.Company>();
                ret = await repo.cache().GetCache();
                if (ret == null)
                    ret = await repo.db().GetAll(rowPage, page, search, isDescending);

                foreach (var item in ret)
                {
                    item.IsActive = true;
                    var oComp = mapper.Map<CompanyModel>(item);
                    lRet.ListCompany.Add(oComp);
                }
                return lRet;
            }
            catch (Exception ex)
            {
                log.LogError("Error call CompanyService.GetAll " + ex.Message);
                context.Status = new Status(StatusCode.NotFound, "Error create company " + ex.Message);
                return null;

            }
        }

        public async override Task<CompanyModel> GetById(reqCompanyById request, ServerCallContext context)
        {
            try
            {
                log.LogInformation($"Begin grpc call CompanyService.GetById");
                var lRet = new resCompanyAll();
                var ret = await repo.cache().GetCache(request.Id);
                if (ret == null)
                {
                    ret = await repo.db().GetById(request.Id);
                    await repo.cache().SetCache(ret);
                }

                return mapper.Map<CompanyModel>(ret);
            }
            catch (Exception ex)
            {
                log.LogError("Error call CompanyService.GetById " + ex.Message);
                context.Status = new Status(StatusCode.NotFound, "Error get id company " + ex.Message);
                return null;
            }
        }

        public override async Task<CompanyEmpty> Update(CompanyModel request, ServerCallContext context)
        {
            try
            {
                log.LogInformation("Call Company service update data");
                var o = mapper.Map<Models.Company>(request);
                var ret = await repo.db().Update(o);

                //remove in cache
                _ = await repo.cache().DeleteCahce(o.Id);
                return new CompanyEmpty { Message = "OK" };
            }
            catch (Exception ex)
            {
                log.LogError("Error call CompanyService.Update " + ex.Message);
                context.Status = new Status(StatusCode.Cancelled, "Error update company " + ex.Message);
                return null;
            }
        }

    }
}
