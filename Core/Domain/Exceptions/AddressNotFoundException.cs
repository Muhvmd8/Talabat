namespace Domain.Exceptions;
public sealed class AddressNotFoundException(string email)
    : NotFoundException($"User with email: {email} doesn't have address!");
