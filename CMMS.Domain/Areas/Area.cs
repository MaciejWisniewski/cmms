using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Areas
{
    public class Area : Entity, IAggregateRoot
    {
        public AreaId Id { get; private set; }
        public string Name { get; private set; }

        private Area()
        {
        }
    }
}
