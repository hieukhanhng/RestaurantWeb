using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.Categories;
[BindProperties]
public class Delete : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public Category Category { get; set; }
    public Delete(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public void OnGet(int id)
    {
        Category = _unitOfWork.Category.GetFirstOrDefault(u=> u.Id==id);
    }

    public async Task<IActionResult> OnPost()
    {
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id ==Category.Id);
        if (categoryFromDb!=null)
        {
            _unitOfWork.Category.Remove(categoryFromDb); 
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        } 
        return Page();
    }
}