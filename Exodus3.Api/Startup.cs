using Exodus3.Api.Data;
using Exodus3.Api.Data.Entities;
using Exodus3.Api.Filters;
using Exodus3.Api.Helpers;
using Exodus3.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Exodus3.Api
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
            var connStr = Configuration.GetConnectionString("E3DbContext");
            services.AddDbContextPool<E3DbContext>(options =>
                options.UseNpgsql(connStr));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<E3DbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                         new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,

                             ValidIssuer = "Exodus3.Security.Bearer",
                             ValidAudience = "Exodus3.Security.Bearer",
                             IssuerSigningKey =
                             JwtSecurityKey.Create("e3IrvLa4Jesus4ever!")
                         };
                });

            services.AddTransient<IRepository<Series>, Repository<Series>>();
            services.AddTransient<IRepository<Sermon>, Repository<Sermon>>();
            services.AddTransient<IRepository<Season>, Repository<Season>>();
            services.AddTransient<ISeriesService, SeriesService>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            })
            .AddJsonOptions(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //  app.UseDeveloperExceptionPage();
            }
            //else 
            //{
            //    app.UseExceptionHandler(builder => 
            //    {
            //        builder.Run(async context =>
            //        {
            //            var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            //            if (exHandlerFeature != null)
            //            {
            //                var logger = loggerFactory.CreateLogger("Global exception logger");
            //                logger.LogError(500, exHandlerFeature.Error, exHandlerFeature.Error.Message);
            //            }

            //            context.Response.StatusCode = 500;

            //        });
            //    });
            //}


            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
