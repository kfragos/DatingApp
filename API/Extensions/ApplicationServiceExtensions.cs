using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            // Register Services (The recommended for HTTP Reqs is AddScoped => for the lifetime of the HTTP Req is alive) 
            services.AddScoped<ITokenService, TokenService>();

            // Connection string to connect with Database 
            string ConnectionStr = config.GetConnectionString("Default");

            // Add DbContext
            services.AddDbContext<DataContext>(options => options.UseMySql(ConnectionStr, ServerVersion.AutoDetect(ConnectionStr)));

            return services;
        }
    }
}
