namespace ECommerceSystem.Application.InputModels
{
    public record PaymentInputModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
}
