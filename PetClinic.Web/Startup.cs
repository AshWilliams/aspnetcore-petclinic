using System.Data.SqlClient;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PetClinicDbContext dbContext, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                var seeder = new DatabaseSeeder(dbContext, logger);
                seeder.Seed();
            }
            
            app.UseMvc();
        }
    }
}