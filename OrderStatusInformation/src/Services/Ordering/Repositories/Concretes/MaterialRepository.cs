using Ordering.Data;
using Ordering.Entities;
using Ordering.Repositories.Contracts;

namespace Ordering.Repositories.Concretes
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
