using MasterData.Models.Dummy;

namespace MasterData.Models
{
    public class Pool{
        /// <summary>
        /// code 1 digit depricated, please user id2 instead
        /// </summary>
        public string Id { get; set; }
        public string Id2 { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string AreaId { get; set; }
        public bool IsActive { get; set; }
        public StatusPool StatusPool { get; set; }
        public string ParentId { get; set; }
        public List<Pool>  Pools { get; set; }
        public List<PoolCompany> Companies { get; set; }
        public DateTime ActiveOn { get; set; }
        public DateTime NonActiveOn { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string UserId { get; set; }
        public string ComputerName { get; set; }
    }

    public enum StatusPool
    {
        SATELITE = 0,
        PARENT = 1
    }
}