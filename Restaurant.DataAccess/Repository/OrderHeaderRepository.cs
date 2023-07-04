using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly AppDbContext _db;
    
    public OrderHeaderRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderHeader orderHeader)
    {
        _db.OrderHeader.Update(orderHeader);
    }

    public void UpdateStatus(int id, string status)
    {
        var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.Status = status;
        }
        
    }
}