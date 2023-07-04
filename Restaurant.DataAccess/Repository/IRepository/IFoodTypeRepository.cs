using Restaurant.Models;

namespace Restaurant.DataAccess.Repository.IRepository;

public interface IFoodTypeRepository : IRepository<FoodType>
{
    void Update(FoodType foodType);
}