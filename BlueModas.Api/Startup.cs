using BlueModas.Api.Database;
using BlueModas.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BlueModas.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(it => it.UseMySql(_configuration.GetConnectionString("Default")));

            services.AddScoped<IProductRepository, EFProductRepository>();

            services.AddControllers();

            services.AddSwaggerGen(it =>
            {
                it.SwaggerDoc("v1", new OpenApiInfo { Title = "Blue Modas API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(it =>
            {
                it.SwaggerEndpoint("/swagger/v1/swagger.json", "Blue Modas API V1");
                it.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(it => it.MapControllers());

            context.Database.Migrate();
        }
    }
}
