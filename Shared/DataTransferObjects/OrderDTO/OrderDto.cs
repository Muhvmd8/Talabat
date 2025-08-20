using Shared.DataTransferObjects.IdentityDTO;

namespace Shared.DataTransferObjects.OrderDTO;
public class OrderDto
{
    public string BasketId { get; set; } = default!;
    public int DeliveryMethodId { get; set; }
    public AddressDto Address { get; set; } = default!;
    public decimal SubTotal { get; set; }
}
