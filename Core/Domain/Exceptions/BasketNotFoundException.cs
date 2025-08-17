namespace Domain.Exceptions;
public sealed class BasketNotFoundException(string id) 
    : NotFoundException($"Basket with id: {id} is not found!");
