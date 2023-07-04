using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.FoodTypes;
[BindProperties]
public class Create : PageModel
{
    public FoodType FoodType { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    public Create(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.FoodType.Add(FoodType);
            _unitOfWork.Save();
            TempData["Success"] = "Food type created successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}