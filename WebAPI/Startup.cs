using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Security;
using Application.Services;
using AutoMapper;
using Domain.Services;
using Domain.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace WebAPI
{
    public class Startup
    {
        readonly string MyAllowedSpecificOrigins = "_myAllowedSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(
                options =>
                {
                    options.AddPolicy(name: MyAllowedSpecificOrigins, builder => {builder.WithOrigins("http://localhost:5001", "http://localhost:3000");});
                }
            );

            /*services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });*/

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });



            services.AddScoped<IObjectFactory, Application.ObjectFactory>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddDbContext<LotteryAppContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("LotteryAppContext"), b => b.MigrationsAssembly("Persistence")));
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //this can get pretty long
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.UserPermissions.TestPermission , builder =>
                {
                    builder.AddRequirements(new PermissionRequirement(Permissions.UserPermissions.TestPermission));
                });

                options.AddPolicy(Permissions.RolePermissions.ListPermissions, builder =>
                {
                    builder.AddRequirements(new PermissionRequirement(Permissions.RolePermissions.ListPermissions));
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowedSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<CorsMiddleware>();
            //app.UseCorsMiddleware();
            /*app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );*/


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
