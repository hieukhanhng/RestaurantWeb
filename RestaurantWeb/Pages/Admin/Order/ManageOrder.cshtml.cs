using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace RestaurantWeb.Pages.Admin.Order;
[Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]

public class ManageOrder : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public List<OrderDetailVM> OrderDetailVm { get; set; }

    public ManageOrder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
        OrderDetailVm = new();
        List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusSubmitted ||
                                                                             u.Status == SD.StatusInProcess).ToList();
        foreach (var item in orderHeaders)
        {
            OrderDetailVM individual = new OrderDetailVM()
            {
                OrderHeader = item,
                OrderDetailsList = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
            };
            OrderDetailVm.Add(individual);
        }
    }

    public IActionResult OnPostOrderInProcess(int orderId)
    {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
        _unitOfWork.Save();
        return RedirectToPage("ManageOrder");
    }
    public IActionResult OnPostOrderReady(int orderId)
    {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
        _unitOfWork.Save();
        return RedirectToPage("ManageOrder");
    }
    public IActionResult OnPostOrderCancel(int orderId)
    {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
        _unitOfWork.Save();
        return RedirectToPage("ManageOrder");
    }
}