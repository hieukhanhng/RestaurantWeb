using Restaurant.Models;

namespace Restaurant.DataAccess.Repository.IRepository;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    void Update(MenuItem menuItem);
}