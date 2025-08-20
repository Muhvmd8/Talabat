namespace Shared.DataTransferObjects.OrderDTO;
public class OrderItemDto
{
    public ProductItemOrderedDto Product { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}