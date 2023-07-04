using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Customer.Home;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<MenuItem> MenuItemList { get; set; }
    public IEnumerable<Category> CategoryList { get; set; }
    public void OnGet()
    {
        MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties:"Category,FoodType");
        CategoryList = _unitOfWork.Category.GetAll(orderby: u => u.OrderBy(c => c.DisplayOrder));
    }
}