using Restaurant.Models;

namespace Restaurant.DataAccess.Repository.IRepository;

public interface IOrderDetailsRepository : IRepository<OrderDetails>
{
    void Update(OrderDetails orderDetails);
}