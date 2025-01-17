using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MerchantAbstraction.AutoMapper;
using MerchantData.Data;
using MerchantService.Images;
using MerchantService.Merchants;
using MerchantService.Products;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MerchantApi
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
                c.SwaggerDoc(name: "V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Merchant API", Version = "V1" });
            });

            services.AddAuthentication(config => {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc", config => {
                    config.Authority = Configuration.GetValue<string>("IdentityUrl");
                    config.ClientId = Configuration.GetValue<string>("ClientId");
                    config.ClientSecret = Configuration.GetValue<string>("ClientSecret");
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.SignedOutCallbackPath = "/Home/Index";

                     // configure cookie claim mapping
                     config.ClaimActions.DeleteClaim("amr");
                    config.ClaimActions.DeleteClaim("s_hash");
                    config.ClaimActions.MapUniqueJsonKey("RawCoding.Grandma", "rc.garndma");

                     // two trips to load claims in to the cookie
                     // but the id token is smaller !
                     config.GetClaimsFromUserInfoEndpoint = true;

                     // configure scope
                     config.Scope.Clear();
                    config.Scope.Add("openid");
                    config.Scope.Add("rc.scope");
                    config.Scope.Add("ApiOne");
                    config.Scope.Add("ApiTwo");
                    config.Scope.Add("offline_access");

                });

            services.AddHttpClient();

            services.AddDbContext<MerchantApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(),typeof(Startup));

            services.AddScoped<IImagesService, ImagesService>();

            services.AddScoped<IMerchantsService, MerchantsService>();

            services.AddScoped<IProductsService, ProductsService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "Merchant API V1");
            });

            DockerMigration.Migrate(app);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
