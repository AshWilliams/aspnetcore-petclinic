using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetClinic.Database;
using Swashbuckle.AspNetCore.Swagger;

namespace PetClinic
{
    public class Startup
    {
        private string ConnectionString { get; }
        
        public IConfiguration Configuration { get; }
    
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var sqlConfig = configuration.GetSection("Sql").Get<SqlConfiguration>();
            var connectionStringBuilder = new SqlConnectionStringBuilder("Server=localhost;Database=PetClinic;")
            {
                UserID = sqlConfig.Username,
                Password = sqlConfig.Password
            };

            ConnectionString = connectionStringBuilder.ConnectionString;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<PetClinicDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });

            services.AddLogging(config => config.AddConsole());
            services.AddMediatR(typeof(DomainPlugin).Assembly);

            services.AddSwaggerGen(swaggerConfig =>
            {
                swaggerConfig.SwaggerDoc("v1", new Info {Title = "Pet Clinic", Version = "v1"});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    swaggerConfig.IncludeXmlComments(xmlPath);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PetClinicDbContext dbContext, ILogger<Startup> logger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Clinic V1");
            });
            
            if (env.IsDevelopment())
            {
                var seeder = new DatabaseSeeder(dbContext, logger);
                seeder.Seed();
            }
            
            app.UseMvc();
        }
    }
}