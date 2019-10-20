using Identity.Data.DataContexts;
using Identity.Data.Repositories;
using Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthContext>(cfg => cfg.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(cfg => cfg.UseSqlServer(_configuration.GetConnectionString("DataConnection")));
            services.AddIdentity<StoreUser, IdentityRole>(c =>
            {
                c.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AuthContext>();

            services.AddControllersWithViews();

            services.AddScoped<ProductRepository>();
            services.AddScoped<SupplierRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                                             "{controller}/{action}/{id?}",
                                             new {controller ="Home", action= "Index"});
            });
        }
    }
}
