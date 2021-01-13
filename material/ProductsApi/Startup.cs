using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ProductsApi
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
            services.AddDbContext<AdventureWorksDbContext>(builder => {

                //var options = builder.UseInMemoryDatabase("FakeDatabase");

                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                var dbOptions = new DbContextOptionsBuilder<AdventureWorksDbContext>()
                                    .UseSqlServer(connectionString).Options;
                                   // .UseInMemoryDatabase("FakeDatabase").Options;

                using var db = new AdventureWorksDbContext(dbOptions);
                db.Database.EnsureCreated();
            });

            services.AddScoped<AdventureWorksDbContext>();
            services.AddScoped<Repositories.ProductRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORS",
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:5001").
                                        AllowAnyHeader().
                                        AllowAnyMethod().
                                        AllowCredentials();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsApi v1"));
            }
            app.UseCors("CORS");
            app.UseHttpsRedirection();

            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
