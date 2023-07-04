using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly AppDbContext _db;
    
    public ShoppingCartRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public int IncrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count += count;
        _db.SaveChanges();
        return shoppingCart.Count;
    }

    public int DecrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count -= count;
        _db.SaveChanges();
        return shoppingCart.Count;
    }
}