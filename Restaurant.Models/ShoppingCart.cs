using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Restaurant.Models;

public class ShoppingCart
{
    public ShoppingCart()
    {
        Count = 1;
    }

    public int Id { get; set; }
    
    public int MenuItemId { get; set; }
    [ForeignKey("MenuItemId")] 
    [ValidateNever]
    public MenuItem MenuItem { get; set; }
    
    [Range(1,50, ErrorMessage = "Please select a number between 1 and 50")]
    public int Count { get; set; }
    
    public string AppUserId { get; set; }    
    [ForeignKey("AppUserId")] 
    [ValidateNever]
    public AppUser AppUser { get; set; }

}