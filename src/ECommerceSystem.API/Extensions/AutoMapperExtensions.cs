using AutoMapper;
using ECommerceSystem.Application.Mappers;

namespace ECommerceSystem.API.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }}
