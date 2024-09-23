using BLL.Authentication;
using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YourProjectNamespace.Models;
using static BLL.Services.UserProfile;

namespace BLL.Services
{
    public static class RepositoryDependancyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddAuthConfig();
            // Register repositories
            services.AddScoped<IRepository<Agency>, Repository<Agency>>();
            services.AddScoped<IRepository<Agent>, Repository<Agent>>();
            services.AddScoped<IRepository<House>, Repository<House>>();
            services.AddScoped<IRepository<Apartment>, Repository<Apartment>>();
            services.AddScoped<IRepository<tasks>, Repository<tasks>>();
            services.AddScoped<IRepository<Villa>, Repository<Villa>>();
            services.AddScoped<IRepository<Subscription>, Repository<Subscription>>();
            services.AddScoped<IRepository<Payment>, Repository<Payment>>();
           // services.AddScoped<IRepository<UserProfile>, Repository<UserProfile>>();
            services.AddScoped<IUserProfile, UserProfile>();
            services.AddScoped<IAuthentication, Authentication>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddAuthConfig(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            // Add Identity Services
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
              //  options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            });

            // Add JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@#$ew786324AhmedAdel9872346AAkvcjfiqwkzxAK")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            return services;
        }
    }
}
