using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

using IDS.Data;
using IDS.Models;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using Newtonsoft.Json;
using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IDS
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

            services.AddControllersWithViews();

            string connStr = Configuration.GetConnectionString("AppDb");

            Action<DbContextOptionsBuilder> dbCtx = (ctx => ctx.UseSqlServer(connStr));

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseSqlServer(connStr)
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning)));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            });

            services.AddIdentityServer(option =>
                option.UserInteraction.LoginUrl = "~/account/login2"
            )
            // services.AddIdentityServer()
            // .AddTestUsers(Config.IDSConfiguration.TestUsers)
            // .AddAspNetIdentity<ApplicationUser>()
            .AddDeveloperSigningCredential()
            .AddConfigurationStore(o =>
            {
                o.ConfigureDbContext = dbCtx =>
                    dbCtx.UseSqlServer(connStr, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(o =>
            {
                o.ConfigureDbContext = dbCtx =>
                    dbCtx.UseSqlServer(connStr, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnSigningOut += signingOutContext =>
                {
                    signingOutContext.CookieOptions.SameSite = SameSiteMode.Lax;
                    return Task.CompletedTask;
                };
            });

            services.AddCors(option => option.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ChangeToken.OnChange(
                () => Configuration.GetReloadToken(),
                (state) => InvokeChanged(state, app),
                env);

            SeedIdentityServerDataFromAppSettings(app); // from appsettings
            // SeedIdentityServerData(app); // from config class
            // SeedIdentityUserData(app, Config.IDSConfiguration.TestUsers).Wait();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }

        private void InvokeChanged(object state, IApplicationBuilder app)
        {
            SeedIdentityServerDataFromAppSettings(app);
        }


        private async Task SeedIdentityUserData(IApplicationBuilder app, List<TestUser> usersList)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var usermng = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var rolemng = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            bool adminRole = await rolemng.RoleExistsAsync("Admin");
            if (!adminRole)
            {
                var role = new IdentityRole { Name = "Admin" };
                await rolemng.CreateAsync(role);
            }

            bool operatorRole = await rolemng.RoleExistsAsync("Operator");
            if (!operatorRole)
            {
                var role = new IdentityRole { Name = "Operator" };
                await rolemng.CreateAsync(role);
            }

            bool userRole = await rolemng.RoleExistsAsync("User");
            if (!userRole)
            {
                var role = new IdentityRole { Name = "User" };
                await rolemng.CreateAsync(role);
            }

            var users = await usermng.Users.CountAsync();
            if (users > 0) return;

            foreach (var u in usersList)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = u.Username,
                    Email = u.Username,
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                };
                var result = await usermng.CreateAsync(user, u.Password);
                if (result.Succeeded)
                {
                    var claim = u.Claims.FirstOrDefault(c => c.Type == "role");
                    await usermng.AddToRoleAsync(user, claim.Value);
                    await usermng.AddClaimsAsync(user, u.Claims);
                }
            }
        }

        private void SeedIdentityServerData(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var configDbCtx = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            configDbCtx.IdentityResources.RemoveRange(configDbCtx.IdentityResources.ToList());
            foreach (var r in Config.IDSConfiguration.IdentityResources)
            {
                configDbCtx.IdentityResources.Add(r.ToEntity());
            }

            configDbCtx.ApiResources.RemoveRange(configDbCtx.ApiResources.ToList());
            foreach (var r in Config.IDSConfiguration.ApiResources)
            {
                configDbCtx.ApiResources.Add(r.ToEntity());
            }

            configDbCtx.ApiScopes.RemoveRange(configDbCtx.ApiScopes.ToList());
            foreach (var s in Config.IDSConfiguration.ApiScopes)
            {
                configDbCtx.ApiScopes.Add(s.ToEntity());
            }

            configDbCtx.Clients.RemoveRange(configDbCtx.Clients.ToList());
            foreach (var c in Config.IDSConfiguration.Clients)
            {
                configDbCtx.Clients.Add(c.ToEntity());
            }

            configDbCtx.SaveChanges();
        }

        private void SeedIdentityServerDataFromAppSettings(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var configDbCtx = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            var clientResult = new List<Client>();
            Configuration.GetSection("IDS:Clients").Bind(clientResult);
            clientResult.ForEach(x => x.ClientSecrets.ToList().ForEach(s => s.Value = s.Value.Sha256()));

            configDbCtx.Clients.RemoveRange(configDbCtx.Clients.ToList());

            foreach (var c in clientResult)
            {
                configDbCtx.Clients.Add(c.ToEntity());
            }

            var IdentityResourceResult = new List<IdentityResource>();
            Configuration.GetSection("IDS:IdentityResources").Bind(IdentityResourceResult);

            configDbCtx.IdentityResources.RemoveRange(configDbCtx.IdentityResources.ToList());

            foreach (var r in IdentityResourceResult)
            {
                configDbCtx.IdentityResources.Add(r.ToEntity());
            }

            var ApiScopesResult = new List<ApiScope>();
            Configuration.GetSection("IDS:ApiScopes").Bind(ApiScopesResult);

            configDbCtx.ApiScopes.RemoveRange(configDbCtx.ApiScopes.ToList());

            foreach (var s in ApiScopesResult)
            {
                configDbCtx.ApiScopes.Add(s.ToEntity());
            }

            var ApiResourcesResult = new List<ApiResource>();
            Configuration.GetSection("IDS:ApiResources").Bind(ApiResourcesResult);

            configDbCtx.ApiResources.RemoveRange(configDbCtx.ApiResources.ToList());

            foreach (var a in ApiResourcesResult)
            {
                configDbCtx.ApiResources.Add(a.ToEntity());
            }


            var UsersResult = new List<TestUser>();
            Configuration.GetSection("IDS:TestUsers").Bind(UsersResult);

            // TODO Find a way to read claims array!!!
            for (int i = 0; i < UsersResult.Count; i++)
            {
                var claims = new List<string>();
                Configuration.GetSection($"IDS:TestUsers:{i}:Claims").Bind(claims);

                foreach (var c in claims)
                {
                    var claim = c.Split("--"); 
                    UsersResult[i].Claims.Add(new Claim(claim[0].ToString(), claim[1].ToString()));
                }                
            }

            SeedIdentityUserData(app, UsersResult).Wait();

            configDbCtx.SaveChanges();
        }
    }
}
