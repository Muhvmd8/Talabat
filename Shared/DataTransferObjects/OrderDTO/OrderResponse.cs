namespace Shared.DataTransferObjects.OrderDTO;
public class OrderResponse
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public AddressDto Address { get; set; } = default!; 
    public string DeliveryMethod { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public ICollection<OrderItemDto> Items { get; set; } = default!;
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
}
