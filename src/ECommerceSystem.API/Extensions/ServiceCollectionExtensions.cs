using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Infrastructure.Persistence.Repositories;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            //Repositories

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services

            return services;
        }
    }
}
