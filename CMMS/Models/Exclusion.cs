using System;

namespace CMMS.Models
{
    public class Exclusion
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int ExclusionTypeId { get; set; }
        public ExclusionType ExclusionType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Note { get; set; }
    }
}
