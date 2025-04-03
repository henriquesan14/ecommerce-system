namespace ECommerceSystem.Application.ViewModels
{
    public record PaymentViewModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
}
