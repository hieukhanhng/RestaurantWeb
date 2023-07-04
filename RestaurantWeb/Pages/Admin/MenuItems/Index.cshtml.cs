using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.MenuItems;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<MenuItem> MenuItems { get; set; }
    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
        MenuItems = _unitOfWork.MenuItem.GetAll();
    }
}