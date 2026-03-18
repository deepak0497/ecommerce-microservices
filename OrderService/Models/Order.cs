

public class Order

{

    public Guid OrderId { get; set; }
    public string UserId    { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } 
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}