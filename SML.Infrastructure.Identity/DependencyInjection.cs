using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SML.Domain.Entities;
using SML.Infrastructure.Identity.Context;
using SML.Infrastructure.Identity.Validators;

namespace SML.Infrastructure.Identity
{
    public static class DependencyInjection
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserValidator<ApplicationUser>, CustomUserValidator>();
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                 b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName))
                );
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;    // уникальный email
                options.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // допустимые символы
                options.Password.RequiredLength = 5;   // минимальная длина
                options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                options.Password.RequireDigit = false; // требуются ли цифры
            }).AddEntityFrameworkStores<IdentityContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/Auth";
            }
            );
        }
    }
}
