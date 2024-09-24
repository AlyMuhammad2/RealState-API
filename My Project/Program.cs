
using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using YourProjectNamespace.Models;

namespace My_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            RepositoryDependancyInjection.AddRepositories(builder.Services);

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
            var app = builder.Build();

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
