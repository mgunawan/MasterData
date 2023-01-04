using System;

namespace MasterData.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CityType { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
    }

    public class Cities
    {
        public Cities()
        {
            cities = new List<City>();
        }

        public List<City> cities {get; set;}
    }
}