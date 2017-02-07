using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using losol.EventR.Data;
using losol.EventR.Models;
using losol.EventR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace losol.EventR
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsecrets.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-losol.EventR-d18f74f1-43ff-46bf-b772-bf590ff69910");
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                    {
                        config.SignIn.RequireConfirmedEmail = true;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequiredLength = 7;
                        config.Password.RequireDigit = true;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add resource folder for localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");

       

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<EmailSenderOptions>(Configuration.GetSection("SendGrid"));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                 //   new CultureInfo("en-US"),
                    new CultureInfo("nb-NO")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "nb-NO", uiCulture: "nb-NO");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                // Custom provider, just returning norwegian, anyway... 
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    // My custom request culture logic
                    return new ProviderCultureResult("nb-NO");
                }));
            });

            services.AddMvc()
           .AddViewLocalization()   // .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)  ?  
           .AddDataAnnotationsLocalization(); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            var supportedCultures = new[]
            {
                //new CultureInfo("en-US"),
                new CultureInfo("nb-NO")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nb-NO"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            // Facebook authentication
            if (!string.IsNullOrWhiteSpace(Configuration["Authentication:Facebook:AppId"]) && !string.IsNullOrWhiteSpace(Configuration["Authentication:Facebook:AppSecret"]))
            {
                app.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = Configuration["Authentication:Facebook:AppId"],
                    AppSecret = Configuration["Authentication:Facebook:AppSecret"],
                    Scope = { "email" },
                    Fields = { "email" },
                    SaveTokens = true,
                    UserInformationEndpoint = "https://graph.facebook.com/v2.8/me?fields=id,name,email",
                    AutomaticAuthenticate = true


                });
            }
            // Google authentication
            if (!string.IsNullOrWhiteSpace(Configuration["Authentication:Google:ClientId"]) && !string.IsNullOrWhiteSpace(Configuration["Authentication:Google:ClientSecret"]))
            {
                app.UseGoogleAuthentication(new GoogleOptions()
                {
                    ClientId = Configuration["Authentication:Google:ClientId"],
                    ClientSecret = Configuration["Authentication:Google:ClientSecret"]
                });
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            await SeedData.EnsureRolesPopulated(app);
            await SeedData.EnsureAdminUserPopulated(app);
        }
    }
}
