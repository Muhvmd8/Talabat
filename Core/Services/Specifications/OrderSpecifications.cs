namespace Services.Specifications;
public class OrderSpecifications : BaseSpecifications<Order, Guid>
{
    public OrderSpecifications(string email)
        : base(o => o.UserEmail == email)
    {
        _AddInculdes(o => o.DeliveryMethod);
        _AddInculdes(o => o.Items);
        _AddOrderByDescending(o => o.OrderDate);
    }

    public OrderSpecifications(Guid orderId)
        : base(o => o.Id == o.Id)
    {
        _AddInculdes(o => o.DeliveryMethod);
        _AddInculdes(o => o.Items);
    }
}
