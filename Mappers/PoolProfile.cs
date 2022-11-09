using AutoMapper;

namespace MasterData.Mappers
{
    public class MasterDataProfile : Profile
    {
        public MasterDataProfile()
        {
            // Source -> Target
            CreateMap<Models.Pool, Pool.Protos.PoolModel>().ReverseMap();
            CreateMap<Models.Area, Area.Protos.AreaModel>().ReverseMap();
            CreateMap<Models.Company, Company.Protos.CompanyModel>().ReverseMap();
            CreateMap<Models.VM.ServiceType, VM.Protos.ServiceTypeModel>().ReverseMap();
        }
    }
}