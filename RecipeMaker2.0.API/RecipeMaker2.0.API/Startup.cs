using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeMaker2._0.Data;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Services;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;

namespace RecipeMaker2._0.API
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
            // Read the connection string from appsettings.
            string dbConnectionString = this.Configuration.GetConnectionString("DefaultConnection");

            // Inject IDbConnection, with implementation from SqlConnection class.
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

            services.AddCors();
            services.AddControllers();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                    .AddJwtBearer(o =>
                    {
                        o.Authority = "https://localhost:5001";
                        o.RequireHttpsMetadata = true;
                        o.Audience = "recipemakerWebApiResource";
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            RoleClaimType = "role"
                        };
                    });

            // Register your regular repositories / services
            services.AddTransient<ISupplierRepo, SupplierRepo>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IMealRepo, MealRepo>();

            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecipeMaker2._0.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeMaker2._0.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
