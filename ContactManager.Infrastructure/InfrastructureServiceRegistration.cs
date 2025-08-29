using ContactManager.Application.Contracts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICsvService<>), typeof(CsvService<>));

            return services;
        }
    }
}