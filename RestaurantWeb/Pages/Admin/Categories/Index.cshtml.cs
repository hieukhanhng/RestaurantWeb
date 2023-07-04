using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.Categories;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<Category> Categories { get; set; }
    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
        Categories = _unitOfWork.Category.GetAll();
        
    }
}