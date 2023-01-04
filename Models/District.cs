using System;

namespace MasterData.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
    }

    public class Districts
    {
        public Districts()
        {
            districts = new List<District>();
        }

        public List<District> districts {get; set;}
    }
}