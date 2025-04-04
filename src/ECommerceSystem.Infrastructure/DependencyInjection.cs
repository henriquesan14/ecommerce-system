using ECommerceSystem.Application.Interfaces;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Infrastructure.Persistence;
using ECommerceSystem.Infrastructure.Persistence.Interceptors;
using ECommerceSystem.Infrastructure.Persistence.Repositories;
using ECommerceSystem.Shared.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDatabase
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            // Add services to the container.
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ECommerceSystemDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<ECommerceSystemDbContext>();

            return services;
        }

        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            //Repositories

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, CachedProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                //options.InstanceName = "Basket";
            });

            return services;
        }
    }
}
