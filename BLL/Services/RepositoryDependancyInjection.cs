using BLL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class RepositoryDependancyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<Agency>, Repository<Agency>>();
            services.AddScoped<IRepository<Agent>, Repository<Agent>>();
            services.AddScoped<IRepository<House>, Repository<House>>();
            services.AddScoped<IRepository<Apartment>, Repository<Apartment>>();
            services.AddScoped<IRepository<tasks>, Repository<tasks>>();
            services.AddScoped<IRepository<Villa>, Repository<Villa>>();
            services.AddScoped<IRepository<Subscription>, Repository<Subscription>>();
            services.AddScoped<IRepository<Payment>, Repository<Payment>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
