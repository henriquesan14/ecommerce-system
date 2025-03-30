namespace ECommerceSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}
