using System;

namespace MasterData.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
    }

    public class Provinces
    {
        public Provinces()
        {
            provinces = new List<Province>();
        }

        public List<Province> provinces {get; set;}
    }
}