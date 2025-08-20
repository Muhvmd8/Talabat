namespace ServicesAbstraction;
public interface IOrderService
{
    Task<OrderResponse> CreateOrder(OrderDto orderDto, string userEmail);
    Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
    Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string email);
    Task<OrderResponse> GetOrderAsync(Guid orderId);
}
