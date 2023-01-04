namespace MasterData.Repositories.MySql
{
    internal static class RegionQuery
    {
        private const string tbProvince = "province";
        private const string fieldProvince = "id,name,created_by,created_at,updated_by,updated_at";
        private const string tbCity = "city";
        private const string fieldCity = "id,name,city_type,province_id,created_by,created_at,updated_by,updated_at";
        private const string tbDistrict = "district";
        private const string fieldDistrict = "id,name,city_id,created_by,created_at,updated_by,updated_at";
        private const string tbSubDistrict = "sub_district";
        private const string fieldSubDistrict = "id,name,postal_code,region_code,district_id,created_by,created_at,updated_by,updated_at";
        
        public const string GetProvince = $"Select {fieldProvince} from {tbProvince} ";
        public const string GetProvinceById = $"{GetProvince} Where id = @Id";
        public const string GetCity = $"Select {fieldCity} From {tbCity} ";
        public const string GetCityById = $"{GetCity} Where id = @Id";
        public const string GetCitiesByProvinceId = $"{GetCity} Where province_id = @Id";
        public const string GetDistrict = $"Select {fieldDistrict} From {tbDistrict} ";
        public const string GetDistrictById = $"{GetDistrict} Where id = @Id";
        public const string GetDistrictsByCityId = $"{GetDistrict} Where city_id = @Id";
        public const string GetSubDistrict = $"Select {fieldSubDistrict} From {tbSubDistrict} ";
        public const string GetSubDistrictById = $"{GetSubDistrict} Where id = @Id";
        public const string GetSubDistrictsByDistrictId = $"{GetSubDistrict} Where district_id = @Id";

        public const string GetRegionById = $@"   
                                            Select sd.id, 
                                                p.id as province_id,
                                                p.name as province_name, 
                                                c.id as city_id,
                                                c.name as city_name, 
                                                c.city_type, 
                                                d.id as district_id, 
                                                d.name as district_name, 
                                                sd.name as sub_district_name, 
                                                sd.postal_code,
                                                sd.region_code
                                            From {tbProvince} p
                                            Inner Join {tbCity} c on c.province_id = p.id
                                            Inner Join {tbDistrict} d on d.city_id = c.id
                                            Inner Join {tbSubDistrict} sd on sd.district_id = d.id
                                            Where sd.id = @Id";
        
    }
}