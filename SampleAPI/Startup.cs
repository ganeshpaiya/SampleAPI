using Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleAPI.Cash;
using Service.Cash;
using Service.Customers;
using Service.Images;
using Service.Orders;
using Service.Products;
using System.Threading.Tasks;

namespace SampleAPI
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
            services.AddSwaggerGen(c =>

           {
               c.SwaggerDoc(name: "V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Sample API", Version = "V1" });
           });

            services.AddDbContext<ApiContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ICustomerService, CustomersService>();

            var redisCashSettings = new RedisCashSettings();
            Configuration.GetSection(nameof(RedisCashSettings)).Bind(redisCashSettings);
            services.AddSingleton(redisCashSettings);

            if(redisCashSettings.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisCashSettings.ConnectionString);
                services.AddSingleton(typeof(ICashService<>), typeof(CashService<>));

            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "Sample API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
