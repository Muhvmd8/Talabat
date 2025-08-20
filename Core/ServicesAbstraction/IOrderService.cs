namespace ServicesAbstraction;
public interface IOrderService
{
    Task<OrderResponse> CreateOrder(OrderDto orderDto, string userEmail);
}
