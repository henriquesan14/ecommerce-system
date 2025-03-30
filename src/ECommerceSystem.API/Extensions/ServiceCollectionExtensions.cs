using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Infrastructure.Persistence.Repositories;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Repositories

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Services

            return services;
        }
    }
}
