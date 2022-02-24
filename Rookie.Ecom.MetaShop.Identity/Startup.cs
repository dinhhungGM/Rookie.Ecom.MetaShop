using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rookie.Ecom.MetaShop.Identity.Data;
using System.Reflection;

namespace Rookie.Ecom.MetaShop.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var defaultConnString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AspNetIdentityDbContext>(options =>
            options.UseSqlServer(defaultConnString,
                b => b.MigrationsAssembly(migrationsAssembly)));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AspNetIdentityDbContext>();
            services.AddIdentityServer()
            .AddAspNetIdentity<IdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}