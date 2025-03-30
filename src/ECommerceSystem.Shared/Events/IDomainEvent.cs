namespace ECommerceSystem.Shared.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
