using AutoMapper;
using CSharpRESTDemo.Entities;
using CSharpRESTDemo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CSharpRESTDemo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
            // services.AddDbContext<DemoDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"));
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DemoDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddCors(opt => {
                opt.AddPolicy("AllowMyOrigin", builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddMvc(opt => opt.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowMyOrigin");
            app.UseMvc();
        }
    }
}