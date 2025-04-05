namespace ECommerceSystem.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }
        public IProductRepository Products { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}
