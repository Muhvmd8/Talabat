namespace Domain.Exceptions;
public sealed class DeliveryMethodNotFoundException(int id)
    : NotFoundException($"No delivery method found with id {id}!");