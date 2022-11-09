using Common.Enums;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

namespace MasterData.Models.Dummy
{
    public class PoolCompany
    {
        public string PoolId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        //public bool IsInduk { get; set; }
        public string  ServiceType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
