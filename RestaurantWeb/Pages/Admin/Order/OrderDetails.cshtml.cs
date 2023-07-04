using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;
using Stripe;

namespace RestaurantWeb.Pages.Admin.Order;

public class OrderDetails : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderDetailVM OrderDetailVM { get; set; }
    public OrderDetails(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet(int id)
    {
        OrderDetailVM = new()
        {
            OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "AppUser"),
            OrderDetailsList = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
        };
    }
    public IActionResult OnPostOrderCompleted(int orderId)
    {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
        _unitOfWork.Save();
        return RedirectToPage("OrderList");
    }
    public IActionResult OnPostOrderRefund(int orderId)
    {
        OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == orderId);
        var options = new RefundCreateOptions
        {
            Reason = RefundReasons.RequestedByCustomer,
            PaymentIntent = orderHeader.PaymentIntentId
        };
        var service = new RefundService();
        Refund refund = service.Create(options);
        
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusRefunded);
        _unitOfWork.Save();
        return RedirectToPage("OrderList");
    }
    public IActionResult OnPostOrderCancel(int orderId)
    {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
        _unitOfWork.Save();
        return RedirectToPage("OrderList");
    }

}