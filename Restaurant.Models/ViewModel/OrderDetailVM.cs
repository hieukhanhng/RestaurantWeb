namespace Restaurant.Models.ViewModel;

public class OrderDetailVM
{
    public OrderHeader OrderHeader { get; set; }
    public List<OrderDetails> OrderDetailsList { get; set; }
}