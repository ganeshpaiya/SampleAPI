using AutoMapper;
using LocationAbstraction.AutoMapper;
using LocationAPI.Middleware;
using LocationData.Data;
using LocationService.Locations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace LocationAPI
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Location", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });


            services.AddDbContext<LocationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddControllers();


            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

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
            services.AddScoped<ILocationsService, LocationsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Location API V1");
            });

            // note this will stop the middleware to catch the errors
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            DockerMigration.Migrate(app);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
