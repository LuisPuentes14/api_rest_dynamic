using api_rest_dynamic.Repository;
using api_rest_dynamic.Services;

namespace api_rest_dynamic.IOC
{
    public static class Dependencies
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {

            // Segregación de interfaces           
            services.AddScoped<IServiceRequests, ServiceRequests>();
            services.AddScoped<IRepositoryRequests, RepositoryRequests>();

        }

    }
}
