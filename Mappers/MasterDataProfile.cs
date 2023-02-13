using AutoMapper;
using MasterData.Models;

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
            CreateMap<Models.Media, Media.Protos.MediaModel>().ReverseMap();
            CreateMap<Models.WorkBackground, Workbackground.Protos.WorkBackgroundModel>().ReverseMap();

            #region Region
            CreateMap<Models.Region, Region.Protos.RegionGrpc>().ReverseMap();
                CreateMap<Models.Province,  Region.Protos.ProvinceGrpc>().ReverseMap();
                CreateMap<Models.Provinces,  Region.Protos.ProvincesGrpc>().ReverseMap();
                CreateMap<Models.City, Region.Protos.CityGrpc>().ReverseMap();
                CreateMap<Models.Cities, Region.Protos.CitiesGrpc>().ReverseMap();
                CreateMap<Models.District, Region.Protos.DistrictGrpc>().ReverseMap();
                CreateMap<Models.Districts, Region.Protos.DistrictsGrpc>().ReverseMap();
                CreateMap<Models.SubDistrict, Region.Protos.SubDistrictGrpc>().ReverseMap();
                CreateMap<Models.SubDistricts, Region.Protos.SubDistrictsGrpc>()
                    .ForMember(dest => dest.SubDistrics, act => act.MapFrom(src => src.subDistricts));
            #endregion
            
        }
    }
}