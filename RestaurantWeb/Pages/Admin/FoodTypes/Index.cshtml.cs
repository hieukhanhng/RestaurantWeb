using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.FoodTypes;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<FoodType> FoodTypes { get; set; }
    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
        FoodTypes = _unitOfWork.FoodType.GetAll();
    }
}