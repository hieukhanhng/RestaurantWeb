using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utility;

namespace RestaurantWeb.Pages.Customer.Home;
[BindProperties]
[Authorize]
public class Details : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Details(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ShoppingCart ShoppingCart { get; set; }
    
    public void OnGet(int id)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCart = new()
        {
            AppUserId = claim.Value,
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType"),
            MenuItemId = id
        };
    }
    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                filter: u => u.AppUserId == ShoppingCart.AppUserId &&
                     u.MenuItemId == ShoppingCart.MenuItemId);
            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(ShoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(
                    u => u.AppUserId == ShoppingCart.AppUserId).ToList().Count);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCart.Count);
            }
            
            return RedirectToPage("Index");
        }
        return Page();
    }
}