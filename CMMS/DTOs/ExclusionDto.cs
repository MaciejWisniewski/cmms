using System;

namespace CMMS.DTOs
{
    public class ExclusionDto
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public int ExclusionTypeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Note { get; set; }
    }
}
