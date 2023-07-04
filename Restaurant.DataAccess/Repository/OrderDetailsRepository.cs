using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
{
    private readonly AppDbContext _db;
    
    public OrderDetailsRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderDetails orderDetails)
    {
        _db.OrderDetails.Update(orderDetails);
    }
    
}