using Digipolis.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using Mowei.Common;
using Mowei.Entities.DbContext;
using Mowei.Entities.Models;
using Mowei.Entities.Repositories;
using Mowei.Resources;
using Mowei.Services;

namespace Mowei
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Mail
            CommEnvironment.SmtpHost = Configuration.GetSection("SmtpSetting").GetValue<string>("SmtpHost");
            CommEnvironment.SmtpPort = Configuration.GetSection("SmtpSetting").GetValue<int>("SmtpPort");
            CommEnvironment.SmtpAccount = Configuration.GetSection("SmtpSetting").GetValue<string>("SmtpAccount");
            CommEnvironment.SmtpPassWord = Configuration.GetSection("SmtpSetting").GetValue<string>("SmtpPassWord");
            //SMS
            CommEnvironment.SmsAccount = Configuration.GetSection("SmsSetting").GetValue<string>("SmsAccount");
            CommEnvironment.SmsPassWord = Configuration.GetSection("SmsSetting").GetValue<string>("SmsPassWord");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityDataAccess<ApplicationDbContext, ApplicationUser, ApplicationRole, Guid>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<TranslatedIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                // User settings
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/LogOut";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.SlidingExpiration = true;
            });

            #region Localization
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                    (x) => string.Format(ValidationResources.CustomValueIsInvalidAccessor, x));
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => string.Format(ValidationResources.CustomValueMustBeANumberAccessor, x));
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => string.Format(ValidationResources.CustomMissingBindRequiredValueAccessor, x));
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (x, y) => string.Format(ValidationResources.CustomAttemptedValueIsInvalidAccessor, x, y));
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => ValidationResources.CustomMissingKeyOrValueAccessor);
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => string.Format(ValidationResources.CustomUnknownValueIsInvalidAccessor, x));
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => string.Format(ValidationResources.CustomValueMustNotBeNullAccessor, x));
                options.ModelMetadataDetailsProviders.Add(new CustomValidationMetadataProvider("Mowei.Resources.ValidationResources", typeof(ValidationResources)));
            })
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("zh"), new CultureInfo("en") };
                options.DefaultRequestCulture = new RequestCulture("zh", "zh");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            #endregion

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<ISmsSender, SmsSender>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddTransient(typeof(IMoweiRepository<>), typeof(MoweiGenericEntityRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile($"Logs/Date.txt");
            #endregion

            #region Localization
            var supportedCultures = new[] { new CultureInfo("zh"), new CultureInfo("en") };
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("zh")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
