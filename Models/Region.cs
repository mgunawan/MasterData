namespace MasterData.Models
{
    public class Region
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CityType { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public string SubDistrictName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
    }
}