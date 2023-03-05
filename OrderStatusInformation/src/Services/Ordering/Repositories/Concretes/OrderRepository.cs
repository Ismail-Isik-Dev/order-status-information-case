using Ordering.Data;
using Ordering.Entities;
using Ordering.Repositories.Contracts;

namespace Ordering.Repositories.Concretes
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeOrderStatus(Order order)
        {
            await Task.Run(() => { _context.Set<Order>().Update(order); });
            await _context.SaveChangesAsync();
        }
    }
}
