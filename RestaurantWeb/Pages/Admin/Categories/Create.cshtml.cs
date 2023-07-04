using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.Categories;
[BindProperties]
public class Create : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public Category Category { get; set; }
    public Create(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(Category); 
            _unitOfWork.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}