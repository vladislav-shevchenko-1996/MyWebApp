using MyWebApp.Services.EmailSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyWebApp.Common;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using MyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace MyWebApp
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
            //string connection = "Server=(localdb)\\mssqllocaldb;Database=localizationdb;Trusted_Connection=True;";
            //services.AddDbContext<LocalizationContext>(options => options.UseSqlServer(connection)); services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            //services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(connection));
            //services.AddControllersWithViews().AddDataAnnotationsLocalization(options => {
            //    options.DataAnnotationLocalizerProvider = (type, factory) =>
            //    factory.Create(null);
            //})
            //.AddViewLocalization(); ;
            services.AddLocalization(options=>options.ResourcesPath="Resources");
            services.AddControllersWithViews().AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("uk-UA")
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddHttpContextAccessor();
            var sendGridKey = Configuration.GetSection("SendGrid:Key");
            Configuration.GetConnectionString("Default");
            services.Configure<SendGridConfiguration>(Configuration.GetSection("SendGrid"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
