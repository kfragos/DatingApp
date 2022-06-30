using API.Data;
using API.Helpers;
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
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // Connection string to connect with Database 
            string ConnectionStr = config.GetConnectionString("Default");

            // Add DbContext
            services.AddDbContext<DataContext>(options => options.UseMySql(ConnectionStr, ServerVersion.AutoDetect(ConnectionStr)));

            return services;
        }
    }
}
