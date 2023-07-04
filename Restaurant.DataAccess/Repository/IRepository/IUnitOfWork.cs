namespace Restaurant.DataAccess.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Category { get; }
    IFoodTypeRepository FoodType { get; }
    IMenuItemRepository MenuItem { get; }
    
    IShoppingCartRepository ShoppingCart { get; }
    IOrderHeaderRepository OrderHeader { get; }
    IOrderDetailsRepository OrderDetails { get; }
    IAppUserRepository AppUser { get; }

    void Save();
}