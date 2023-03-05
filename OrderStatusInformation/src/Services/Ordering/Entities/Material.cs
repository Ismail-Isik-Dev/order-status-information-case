using Ordering.Comman;

namespace Ordering.Entities
{
    public class Material : BaseDomainEntity, IEntity
    {
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
