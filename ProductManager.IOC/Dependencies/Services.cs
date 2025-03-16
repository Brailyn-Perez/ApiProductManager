using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductManager.BL.Interfaces.Category;
using ProductManager.BL.Interfaces.Product;
using ProductManager.BL.Interfaces.Supplier;
using ProductManager.BL.Interfaces.User;
using ProductManager.BL.Services.Category;
using ProductManager.BL.Services.Product;
using ProductManager.BL.Services.Supplier;
using ProductManager.BL.Services.User;
using ProductManager.DAL.Interfaces.Repositories;
using ProductManager.DAL.Repositories;
using System.Text;

namespace API_WhitJsonWebToken_JWT_.API.Services
{
    public static class Services
    {

        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region "Context Config"
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("db")));
            #endregion

            #region "JWS Config"
            services.AddSingleton<IJwtService , JwtService>();
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = false;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]!))
                };
            });
            #endregion

            #region "Services Config"
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            #endregion

            #region "Repositories Config"
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            #endregion
            return services;
        }
    }
}