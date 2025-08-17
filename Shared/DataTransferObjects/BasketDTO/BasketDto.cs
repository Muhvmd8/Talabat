namespace Shared.DataTransferObjects.BasketDTO;
public class BasketDto
{
    public string Id { get; set; }
    public ICollection<BasketItemResponse> Items { get; set; } = [];
}
