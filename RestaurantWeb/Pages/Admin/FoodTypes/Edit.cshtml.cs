using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.FoodTypes;
[BindProperties]
public class Edit : PageModel
{
    
    public FoodType FoodType { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    public Edit(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    public void OnGet(int id)
    {
        FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u=> u.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.FoodType.Update(FoodType);
            _unitOfWork.Save();
            TempData["Success"] = "Food Type updated successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}