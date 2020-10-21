using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.Infrastructure.Services;
using WebStore1p.Infrastructure.Services.InSQL;

using Microsoft.EntityFrameworkCore;
//��� �� �����!!!
using WebStore.DAL.Context;

using WebStore1p.Data;

using Microsoft.AspNetCore.Identity;

using WebStore1p.Domain.Identity;

using WebStore1p.Infrastructure.Services.InCookies;

using System;


namespace WebStore1p
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        //����������� � ������� ����� ��������� ������� ������� � �������� ����� �������� � ���������
        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        //�������� � ���������� ��������
        public void ConfigureServices(IServiceCollection services)
        {
            //�� �� ��������� ������������� ��
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //L6 Identity ����������� ������� ������������� � �����
            // ���� ������ �� services.AddIdentity<IdentityUser, IdentityRole>();, ���� �����������, ��� ����
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>() //��� ������� ������ Identity, �� � ���������� �� ����� ������ ��������� ��, ������ �������
                .AddDefaultTokenProviders(); //������������ ����������� ��������

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxABCD...1234567890";
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                //opt.Cookie.Expiration = TimeSpan.FromDays(10); - ��� ���������������� � 3.1
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                //L6 Identity ���� �� ������� ������� ����� �������������� ���������������� ������������� �� ���������� Account
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true; //L6 �� �������� ������� �������� ������� ������������� ��������� ������������� ������ ������������, ��� �� �������������
            });

            //�� ��������� �������������� ��
            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //�� ���� 3 ��������� �������� ����������� �������� 
            //�� �.AddTransient - ������ ��� ����� ����������� ��������� �������
            //�� �.AddScoped - ���� ��������� �� ������� ���������
            //�� �.AddSingleton - ���� ��������� �� �� �������
            //�� ��������� ���� ������, ������� �������� ���� ������ ����� ����� ����������, 
            //�� �.�. ���������� ���������� � ������ ������ �� ���������� ���� ����� �������� ����������� ����������, 
            //�� � ������ ������ ������� � ����������� ����� �����������

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SQLProductData>();

            services.AddScoped<ICartService, CookiesCartService>();
        }

        //�� � ���� ������ �� ����� ��������� ��� ������� � �������� ����� ���� ���� ����������, � ���������� �� ������� ������������� ��
        //�� ������������� ��������, ���� ����� ��������� ������������� ��
        //�� ��� ������� app ����������� ������ ����������, ������� �� ����� ���� ������
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
            //5 �� ������������� ��
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            //�� �������� ��������� ������ � ������� ������ ����������
            
            app.UseRouting();

            //L6 �������� Identity ������������� ��
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/welcome");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "Home/Index/id"); //http://localhost:5000/Home/Index/id
                    pattern: "{controller=Home}/{action=Index}/{id?}"); //http://localhost:5000/Home/Index/id
            });

        }
    }
}
