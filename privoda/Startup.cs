using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using privoda.Models;
using Microsoft.EntityFrameworkCore;
using privoda.Utils;
using privoda.Services;
using privoda.Contracts.Services;
using privoda.Contracts.Repositories;
using privoda.Data.Repositories;
using System.Globalization;

namespace privoda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PrivodaDbContext>(options => options.UseSqlServer(connection));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<EmailService>();
            services.AddTransient<IService<ModelLine>, Service<ModelLine>>();
            services.AddTransient<IService<LineType>, Service<LineType>>();
            services.AddTransient<IService<Description>, Service<Description>>();
            services.AddTransient<IService<PowerAndCurrent>, Service<PowerAndCurrent>>();
            services.AddTransient<IService<CurrentType>, Service<CurrentType>>();
            services.AddTransient<IService<CircuitBreaker>, Service<CircuitBreaker>>();
            services.AddTransient<IService<Contactor>, Service<Contactor>>();
            services.AddTransient<IService<Coil>, Service<Coil>>();
            services.AddTransient<TeSysService>();

            services.AddTransient<IRepository<ModelLine>, Repository<ModelLine>>();
            services.AddTransient<IRepository<LineType>, Repository<LineType>>();
            services.AddTransient<IRepository<Description>, Repository<Description>>();
            services.AddTransient<IRepository<PowerAndCurrent>, Repository<PowerAndCurrent>>();
            services.AddTransient<IRepository<CurrentType>, Repository<CurrentType>>();
            services.AddTransient<ICoilRepository, CoilRepository>();
            services.AddTransient<IRepository<CircuitBreaker>, Repository<CircuitBreaker>>();
            services.AddTransient<IRepository<Contactor>, Repository<Contactor>>();
            services.AddTransient<IRepository<Coil>, Repository<Coil>>();

            // Add functionality to inject IOptions<T>
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "€";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
