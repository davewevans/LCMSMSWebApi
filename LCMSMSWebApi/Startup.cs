using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LCMSMSWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Turn off Identity mapping
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<IFileStorageService, AzureStorageService>();

            services.AddScoped<ImageService>();

            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            //builder.UserID = Configuration["UserID"];
            //builder.Password = Configuration["Password"]; 

            //services.AddDbContext<LocalApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("LocalDefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // IdentityServer 4 configuration
            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //})
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<IdentityUser, ApplicationDbContext>()
            //    .AddProfileService<IdentityProfileService>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                      ClockSkew = TimeSpan.Zero,
                  };
              });


            //
            // TODO remove for productions
            // Paul's db
            //
            //builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("PaulConnection"));
            //builder.UserID = Configuration["PaulUserID"];
            //builder.Password = Configuration["PaulPassword"];
            //services.AddDbContext<PaulDbContext>(option =>
            //    option.UseSqlServer(Configuration.GetConnectionString("PaulConnection")));

            services.AddScoped<ISyncDatabasesService, SyncDatabasesService>();         


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LCMSMS API", Version = "v1", });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LCMSMS API");
            });

            app.UseHttpsRedirection();           

            
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            // app.UseIdentityServer();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
