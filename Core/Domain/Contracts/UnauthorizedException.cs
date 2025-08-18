namespace Domain.Contracts;
public class UnauthorizedException(string message = "Invalid email or password!") : Exception(message);
