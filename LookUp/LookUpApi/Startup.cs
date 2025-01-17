using LookUpData.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;
using LookUpService;
using LookUpAbstraction.AutoMapper;

namespace LookUpApi
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
            //Adding REFERENCE to the DBCONTEXT
            services.AddDbContextPool<LookUpDbContext>(
                options => options.UseSqlServer(
                  Configuration.GetConnectionString("Default")
                ));

            //Adding REFERENCE TO Automapper
            //This will scan the assembly in which startup is found. 
            //and tries to find a profiler class that configures automapper
            // we had to use c=> c.addprofile because it's in another project
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfile>(), typeof(Startup));


            //Adding Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LookUp API", Version = "v1" });
            });

            //Adding DI to lookup service package
            services.AddScoped<ILookUpsService, LookUpsService>();
            services.AddScoped<ILookUpTypesService, LookUpTypesService>();
            services.AddScoped<ICascadingLookUpsService, CascadingLookUpsService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger Configuration
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LookUp API V1");
            });
            #endregion

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            DockerMigration.Migrate(app);

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
