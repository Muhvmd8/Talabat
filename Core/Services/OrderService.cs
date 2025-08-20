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
    public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
    {
        var deliverMethodRepository = _unitOfWork.GetRepository<DeliveryMethod, int>();
        var deliveryMethods = await deliverMethodRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
    }
    public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string email)
    {
        var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
        var specifications = new OrderSpecifications(email);
        var orders = await orderRepository.GetAllAsync(specifications);
        return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(orders); 
    }
    public async Task<OrderResponse> GetOrderAsync(Guid orderId)
    {
        var orderRepository = _unitOfWork.GetRepository<Order, Guid>();

        var specifications = new OrderSpecifications(orderId);
        var order = await orderRepository.GetByIdAsync(specifications) ?? 
                throw new OrderNotFoundException(orderId);

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