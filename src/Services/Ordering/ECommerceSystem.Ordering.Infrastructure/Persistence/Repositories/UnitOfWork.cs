using ECommerceSystem.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly ECommerceSystemDbContext _dbContext;

        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(ECommerceSystemDbContext dbContext, IOrderRepository orders, IOrderItemRepository orderItems, IProductRepository products)
        {
            _dbContext = dbContext;
            Orders = orders;
            OrderItems = orderItems;
            Products = products;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int> CompleteAsync()
        {
            var result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public void Dispose()
        {
            IsDisposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void IsDisposing(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
