using AutoMapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MT.RabbitMq;
using MT.ReportService.API.AllExtentions;
using MT.ReportService.API.Consumer;
using MT.ReportService.Core.Interfaces.Repositories;
using MT.ReportService.Core.Interfaces.Services;
using MT.ReportService.Core.Interfaces.UnitOfWork;
using MT.ReportService.Data.Context;
using MT.ReportService.Data.Repositories;
using MT.ReportService.Data.UnitOfWork;
using MT.ReportService.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.ReportService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

          

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("NpgConStr"), o => o.MigrationsAssembly("MT.ReportService.Data")));
            services.AddTransient<ConsumerService>();
            services.AddCap(options =>
            {
                options.UseDashboard(o => o.PathMatch = "/cap-dashboard");
                options.UsePostgreSql(Configuration.GetConnectionString("NpgConStr"));
                options.UseEntityFramework<AppDbContext>();
                options.UseRabbitMQ(BusConstants.RabbitMqUri);
                
            });

            services.AddUnitOfWork<AppDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MT.ReportService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT.ReportService.API v1"));
            }
            app.UseCapDashboard();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            AutoMigrate(app);
        }

        private static void AutoMigrate(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context?.Database.Migrate();
        }
    }
}
