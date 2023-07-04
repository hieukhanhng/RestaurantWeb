using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utility;

namespace RestaurantWeb.Pages.Customer.Cart;
[Authorize]
public class Index : PageModel
{
    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
    public double CartTotal { get; set; }
    private readonly IUnitOfWork _unitOfWork;

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        CartTotal = 0;
    }


    public void OnGet()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claim!=null)
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.AppUserId == claim.Value, 
                includeProperties:"MenuItem,MenuItem.FoodType,MenuItem.Category");
            foreach (var cartItem in ShoppingCartList)
            {
                CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
            }
        }
    }

    public IActionResult OnPostPlus(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
        return RedirectToPage("/Customer/Cart/Index");
    }
    
    public IActionResult OnPostMinus(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        if (cart.Count == 1)
        {
            OnPostRemove(cartId);
        }
        _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
        return RedirectToPage("/Customer/Cart/Index");
    }
    public IActionResult OnPostRemove(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        var count = _unitOfWork.ShoppingCart.GetAll(
            u => u.AppUserId == cart.AppUserId).ToList().Count-1;
        _unitOfWork.ShoppingCart.Remove(cart);
        _unitOfWork.Save();
        HttpContext.Session.SetInt32(SD.SessionCart, count);
        return RedirectToPage("/Customer/Cart/Index");
    }

}