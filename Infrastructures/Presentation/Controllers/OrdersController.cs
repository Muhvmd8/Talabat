namespace Presentation.Controllers;
public class OrdersController(IServiceManager serviceManager) 
    : ApiController
{
    // Create Order 
    [Authorize]
    [HttpPost("CreateOrder")]
    public async Task<ActionResult<OrderResponse>> CreateOrder(OrderDto orderDto)
    {
        var orderResponse = await serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());
        return Ok(orderResponse);
    }
    // Get Delivery Methods
    [Authorize]
    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetAllDeliveryMethods()
    {
        var deliveryMethods = await serviceManager.OrderService.GetAllDeliveryMethodsAsync();
        return Ok(deliveryMethods);
    }
    // Get All Orders For Specific User 
    [Authorize]
    [HttpGet("Orders")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllOrders()
    {
        var orders = await serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
        return Ok(orders);
    }
    // Get Order By Id For Specific User
    [Authorize]
    [HttpGet("Order")]
    public async Task<ActionResult<OrderResponse>> GetOrder(Guid orderId)
    {
        var order = await serviceManager.OrderService.GetOrderAsync(orderId);
        return Ok(order);
    }
}