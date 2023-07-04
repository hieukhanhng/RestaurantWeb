using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Restaurant.Models;

public class OrderHeader
{
    [Key] public int Id { get; set; }
    
    [Required] public string UserId { get; set; }
    
    [ForeignKey("UserId")] 
    [ValidateNever]
    public AppUser AppUser { get; set; }
    
    [Required] public DateTime OrderDate { get; set; }
    
    [Required]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Display(Name = "Order Total")]
    public double OrderTotal { get; set; }
    
    [Required]
    [Display(Name = "Pick Up Time")]
    public DateTime PickupTime { get; set; }

    [Required]
    [NotMapped]
    public DateTime PickupDate { get; set; }

    public string Status { get; set; }
    
    public string? Comments { get; set; }

    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }
    
    [Required]
    [Display(Name = "Pickup Name")] public string PickupName { get; set; }
    [Required]
    [Display(Name = "Phone Number")] public string PhoneNumber { get; set; }
}