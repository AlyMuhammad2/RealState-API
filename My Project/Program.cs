
using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My_Project.Common;
using Stripe;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace My_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            RepositoryDependancyInjection.AddRepositories(builder.Services);
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddControllers();

            //builder.Services.AddIdentity<User, IdentityRole>()
            //                .AddEntityFrameworkStores<Context>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Context>(options =>
                                 options.UseSqlServer(builder.Configuration.GetConnectionString("DEV")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            builder.Services.AddRepositories();
            builder.Services.AddAuthConfig();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("mypolicy", policy => policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());}); 
            var app = builder.Build();
            app.UseCors("mypolicy");
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
