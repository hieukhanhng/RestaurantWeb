using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.FoodTypes;
[BindProperties]
public class Delete : PageModel
{
    public FoodType FoodType { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    public Delete(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }
    public void OnGet(int id)
    {
        FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        var foodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == FoodType.Id);
        if (foodTypeFromDb!=null)
        {
            _unitOfWork.FoodType.Remove(foodTypeFromDb); 
            _unitOfWork.Save();
            TempData["Success"] = "Food type deleted successfully";
            return RedirectToPage("Index");
        } 
        return Page();
    }
}