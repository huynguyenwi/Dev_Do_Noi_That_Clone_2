using core.Services;
using csdl.web.Services;

namespace csdl.web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            // Đăng ký service
            services.AddTransient<ProductService_I, ProductService>();
            services.AddTransient<Product_CatService_I, Product_CatService>();

            // Session
            services.AddDistributedMemoryCache();
            services.AddSession();

            // HttpContext
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
