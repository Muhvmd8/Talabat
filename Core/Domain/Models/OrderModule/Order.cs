namespace Domain.Models.OrderModule;
public class Order : BaseEntity<Guid>
{
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderAddress Address { get; set; } = default!; // 1 : 1 => Mandatory
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public int DeliveryMethodId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal GetTotal() => SubTotal + DeliveryMethod.Price; // Derived Attribute
}
