namespace Domain.Models.BasketModule;
public class CustomerBasket
{
    public string Id { get; set; } 
    public ICollection<BasketItem> Items { get; set; }
}
