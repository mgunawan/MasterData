using System;

namespace MasterData.Models
{
    public class SubDistrict
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PostalCode { get; set; }
        public string RegionCode { get; set; }
        public int DistrictId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
    }

    public class SubDistricts
    {
        public SubDistricts()
        {
            subDistricts = new List<SubDistrict>();
        }

        public List<SubDistrict> subDistricts {get; set;}
    }
}