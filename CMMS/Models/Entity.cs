namespace CMMS.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
