using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Employees;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Users;
using WebStore.Clients.Values;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Models;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.Services;
using WebStore.Services;
using WebStore.Services.Data;
using WebStore.Services.InMemory;
using WebStore.Services.SQL;
using WebStore.Logger;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConection")));
            //services.AddTransient<WebStoreContextInitializer>();

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<ICartService, CookieCartService>();
            services.AddScoped<IOrderService, OrdersClient>();

            services.AddTransient<IValuesService, ValuesClient>();

            /*
             
            IUserRoleStore<User>,   
            IUserClaimStore<User>, 
            IUserPasswordStore<User>, 
            IUserTwoFactorStore<User>, 
            IUserEmailStore<User>, 
            IUserPhoneNumberStore<User>, 
            IUserLoginStore<User>, 
            IUserLockoutStore<User> 

             */

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    // конфигурация cookies возможна здесь
                })
                //.AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            #region Identity - собственная реализация хранилищ данных на основе WebAPI

            services.AddTransient<IUserStore<User>, UserClient>();
            services.AddTransient<IUserRoleStore<User>, UserClient>();
            services.AddTransient<IUserClaimStore<User>, UserClient>();
            services.AddTransient<IUserPasswordStore<User>, UserClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UserClient>();
            services.AddTransient<IUserEmailStore<User>, UserClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UserClient>();
            services.AddTransient<IUserLoginStore<User>, UserClient>();
            services.AddTransient<IUserLockoutStore<User>, UserClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RoleClient>();

            #endregion

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredUniqueChars = 3;

                cfg.Lockout.MaxFailedAccessAttempts = 10;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                cfg.Lockout.AllowedForNewUsers = true;

                cfg.User.RequireUniqueEmail = false; // грабли!
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.HttpOnly = true;
                cfg.Cookie.Expiration = TimeSpan.FromDays(150);
                cfg.Cookie.MaxAge = TimeSpan.FromDays(150);

                cfg.LoginPath = "/Account/Login";
                cfg.LogoutPath = "/Account/Logout";
                cfg.AccessDeniedPath = "/Account/AccessDenied";

                cfg.SlidingExpiration = true;
            });

            services.AddMvc(opt =>
            {
                //opt.Filters.Add<ActionFilter>();
                //opt.Conventions.Add(new TestConvention());
            });

            services.AddAutoMapper(opt =>
            {
                opt.CreateMap<Employee, Employee>();
            });

            //AutoMapper.Mapper.Initialize(opt =>
            //{
            //    opt.CreateMap<Employee, Employee>();
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            //app.UseWelcomePage("/Welcome");

            app.UseAuthentication();

            //app.UseMvcWithDefaultRoute(); // "default" : "{controller=Home}/{action=Index}/{id?}"
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"/*,
                    defaults: new
                    {
                        controller = "Home", 
                        action = "Index",
                        id = (int?)null
                    }*/);
            });
        }
    }
}
