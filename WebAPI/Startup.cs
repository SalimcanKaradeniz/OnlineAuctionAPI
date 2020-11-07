using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineAuction.Core.Extensions;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.FormSettingsService;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.SiteSettingsService;
using OnlineAuction.Services.SocialMediaSettingsService;
using OnlineAuction.Services.Users;

namespace WebAPI
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

            AppSettings appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.Configure<AppSettings>(Configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #region Context

            services.AddDbContext<OnlineAuctionContext>(options => options.UseSqlServer(appSettings.ConnectionStrings.SQLConnection, b => b.MigrationsAssembly("Data"))).AddUnitOfWork<OnlineAuctionContext>();

            #endregion

            #region Jwt

            //appSettings.JwtConfiguration.SigningKey
            var key = Encoding.ASCII.GetBytes(appSettings.JwtConfiguration.SigningKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = appSettings.JwtConfiguration.Audience;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.ClaimsIssuer = appSettings.JwtConfiguration.Issuer;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (context) =>
                    {
                        //context.Principal.Identity is ClaimsIdentity
                        //So casting it to ClaimsIdentity provides all generated claims
                        //And for an extra token validation they might be usefull
                        var name = context.Principal.Identity.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            context.Fail("Unauthorized. Please re-login");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILanguagesService, LanguagesService>();
            services.AddTransient<ISiteSettingsService, SiteSettingsService>();
            services.AddTransient<IFormSettingsService, FormSettingsService>();
            services.AddTransient<ISocialMediaSettingsService, SocialMediaSettingsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}