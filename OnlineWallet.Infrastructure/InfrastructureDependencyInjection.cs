using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineWallet.Application.Common.Handlers;
using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Infrastructure.Handlers;
using OnlineWallet.Infrastructure.Persistence;
using OnlineWallet.Infrastructure.Services;
using System.Text;

namespace OnlineWallet.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(configuration);
            services.AddPersistence(configuration);
            services.AddSingleton<IBalanceManager, BalanceManager>();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
            });

            services.AddScoped<Func<ApplicationDbContext>>((provider) => provider.GetRequiredService<ApplicationDbContext>);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, Handlers.TokenHandler>();
            services.AddScoped<IPasswordHandler, PasswordHandler>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secrets:JwtToken").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            return services;
        }
    }
}
