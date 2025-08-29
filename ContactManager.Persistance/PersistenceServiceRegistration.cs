using ContactManager.Application.Contracts.Persistance;
using ContactManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactManagerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
            ("ContactManagerConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}