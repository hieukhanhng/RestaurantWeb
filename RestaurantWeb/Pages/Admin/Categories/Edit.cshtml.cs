using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.Categories;
[BindProperties]
public class Edit : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public Category Category{ get; set; }
    public Edit(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public void OnGet(int id)
    {
        Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(Category);
            _unitOfWork.Save();
            TempData["Success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}