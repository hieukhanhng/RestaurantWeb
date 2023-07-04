using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
{
    private readonly AppDbContext _db;
    
    public MenuItemRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(MenuItem menuItem)
    {
        var objFromDb = _db.MenuItem.FirstOrDefault(u => u.Id == menuItem.Id);
        objFromDb.Name = menuItem.Name;
        objFromDb.Description = menuItem.Description;
        objFromDb.Price = menuItem.Price;
        objFromDb.CategoryId = menuItem.CategoryId;
        objFromDb.FoodTypeId = menuItem.FoodTypeId;
        if (objFromDb.Image != null)
        {
            objFromDb.Image = menuItem.Image;
        }
    }
    
}