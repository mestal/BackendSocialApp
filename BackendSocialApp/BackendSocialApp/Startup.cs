﻿using System.Text;
using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Persistence.Contexts;
using BackendSocialApp.Persistence.Repositories;
using BackendSocialApp.Resources;
using BackendSocialApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BackendSocialApp.Tools;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Logging;
using BackendSocialApp.Extensions;

namespace BackendSocialApp
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
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                //içiçe objelerde sonsuz döngüye giriyorsa ikinci seferden sonra boş döndürmek istiyorsan alttakini aç
                //.AddJsonOptions(option =>
                //{
                //    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //});

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            //services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>();

            services.AddDefaultIdentity<ApplicationUser>()
                        .AddRoles<ApplicationRole>()
                        .AddRoleManager<RoleManager<ApplicationRole>>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                }
            );

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            }
             );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoffeeFortuneTellingService, CoffeeFortuneTellingService>();
            services.AddScoped<ICoffeeFortuneTellingRepository, CoffeeFortuneTellingRepository>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IFeedRepository, FeedRepository>();
            services.AddSingleton<IEmailHelper, EmailHelper>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, AppDbContext context, ILogger<Startup> logger)
        {
            app.UseCors(x => x
            .WithOrigins("capacitor://localhost",
              "ionic://localhost",
              "http://localhost",
              "http://localhost:8080",
              "http://localhost:8100",
              "http://localhost:8000",
              //"*",
              "https://falcim.xyz",
              "http://falcim.xyz",
              "https://www.falcim.xyz",
              "http://www.falcim.xyz"
              )
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            //.AllowCredentials()
            .SetIsOriginAllowed(hostName => true));

            //For 204 errors
            //app.Use(async (ctx, next) =>
            //{
            //    await next();
            //    if(ctx.Response.StatusCode == 204)
            //    {
            //        ctx.Response.ContentLength = 0;
            //    }
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);

            //var url = Configuration["ApplicationSettings:JWT_Secret"].ToString();

            app.UseAuthentication();

            if(Configuration.GetValue<bool>("InitializeSampleData"))
            {
                IdentityDataInitializer.SeedData(userManager, roleManager, context);
            }

            app.UseMvc();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath + "/", Configuration.GetValue<string>("FileServerRequestPath"))),
                RequestPath = "/Assets",
                EnableDirectoryBrowsing = false
            });
        }
    }
}
