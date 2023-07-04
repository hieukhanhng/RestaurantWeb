using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _db;
    
    public CategoryRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category category)
    {
        var objFromDb = _db.Category.FirstOrDefault(u => u.Id == category.Id);
        objFromDb.Name = category.Name;
        objFromDb.DisplayOrder = category.DisplayOrder;
    }
    
}