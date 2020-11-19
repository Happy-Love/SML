using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SML.Application.Interfaces.Repositories;
using SML.Infrastructure.Persistence.Context;
using SML.Infrastructure.Persistence.Repository;

namespace SML.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SMLContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder=>builder.MigrationsAssembly(typeof(SMLContext).Assembly.FullName))
                );
        }
    }
}
