using Ordering.Entities;

namespace Ordering.Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task ChangeOrderStatus(Order order);
    }
}
