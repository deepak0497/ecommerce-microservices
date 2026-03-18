


using Microsoft.AspNetCore.Mvc;
using OrderService.Clients;
using OrderService.Services;
using StackExchange.Redis;
using System.Threading.Tasks;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderServiceHandler _orderService;
    private readonly ICartServiceClient _cartServiceClient;
    private readonly IProductServiceClient _productClient;

    public OrderController(OrderServiceHandler orderService, ICartServiceClient cartServiceClient, IProductServiceClient productClient)

    {
        _cartServiceClient = cartServiceClient;
        _productClient = productClient;
        _orderService = orderService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        var cart =await  _cartServiceClient.GetCart(order.UserId);

        order.OrderId = Guid.NewGuid();
        order.OrderDate = DateTime.UtcNow;

        decimal total = 0;
        Console.WriteLine("Cart is" + cart);
        foreach(var item in cart)
        {
            var product = await _productClient.GetProduct(item.ProductId);
            total += product.Price * item.Quantity; 
        }
        order.TotalAmount = total;

        _orderService.PublishOrderCreatedEvent(order);
        return Ok(order);

    }
}