using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
{
    private readonly AppDbContext _db;
    
    public FoodTypeRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(FoodType foodType)
    {
        var objFromDb = _db.FoodType.FirstOrDefault(u => u.Id == foodType.Id);
        objFromDb.Name = foodType.Name;
    }
    
}