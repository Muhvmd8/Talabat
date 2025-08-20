namespace Domain.Contracts;
public sealed class OrderNotFoundException(Guid id)
    : NotFoundException($"Order with id {id} is not found!");