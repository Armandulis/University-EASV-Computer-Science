using Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PetShop.Core.AplicationService;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Services;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using PetShop.Infrastucture.Data;
using PetShop.Infrastucture.Data.Repositories;
using System;
using System.Collections.Generic;

namespace PetRestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment Environment)
        {
            Configuration = configuration;
            env = Environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //true if we use other sources, facebook, google
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JWTSecurityKey.Key,
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
            
            if (env.IsDevelopment())
            {
            }
            else
            {
                // Azure SQL database:
                services.AddDbContext<PetShopContext>(opt =>
                         opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            } 




            /*services.AddDbContext<PetShopContext>(
                option => option.UseInMemoryDatabase("Databasu")
                );*/

            services.AddDbContext<PetShopContext>(
                option => option.UseSqlite("Data Source=petShopApp.db"));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            
            services.AddMvc();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope()) {
                    var ctx = scope.ServiceProvider.GetService<PetShopContext>();
                    InitialiseDB.SeedDB(ctx);
                } 
            }
            else
            {
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
