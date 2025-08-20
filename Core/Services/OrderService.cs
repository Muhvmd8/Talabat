using Order = Domain.Models.OrderModule.Order;
namespace Services;
public class OrderService(IMapper _mapper, IUnitOfWork _unitOfWork, IBasketRepository _basketRepository) 
    : IOrderService
{
    public async Task<OrderResponse> CreateOrder(OrderDto orderDto, string userEmail)
    {
        // Shipping Address
        var orderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

        // Delivery Method
        var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();

        var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(orderDto.DeliveryMethodId)?? 
            throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

        // User Basket
        var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)??
            throw new BasketNotFoundException(orderDto.BasketId);

        // Set Order Items
        List<OrderItem> orderItems = [];
        var productRepository = _unitOfWork.GetRepository<Product, int>();

        foreach(var item in basket.Items)
        {
            // Get Product By Id
            var product = await productRepository.GetByIdAsync(item.Id) ??
                throw new ProductNotFoundException(item.Id);

            var orderItem = _CreateOrderItem(item, product);

            orderItems.Add(orderItem);
        }

        // Create Sub-Total
        var subTotal = orderItems.Sum(i => i.Quantity *  i.Price);

        // Create Order Object
        var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);

        // Add Order Record To Database
        var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
        await orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Order, OrderResponse>(order);
    }
    private static OrderItem _CreateOrderItem(BasketItem item, Product product)
        => new OrderItem
           {
                Price = product.Price,
                Product = new ProductItemOrdered
                {
                    PictureUrl = product.PictureUrl,
                    ProductName = product.Name,
                    ProductId = product.Id
                },
                Quantity = item.Quantity
           };
}