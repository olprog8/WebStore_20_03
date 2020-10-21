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
//Что за жесть!!!
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
        //конструктор в котором можно запросить базовые объекты с которыми можно работать и окружение
        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        //параметр с коллекцией сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            //БД ПШ настройка инициализации БД
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //L6 Identity Регистрация сервиса Пользователей и Ролей
            // если быстро то services.AddIdentity<IdentityUser, IdentityRole>();, либо собственные, как ниже
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>() //Где хранить данные Identity, но в приложении мб более одного контекста БД, иногда бываает
                .AddDefaultTokenProviders(); //Регистрируем провайдеров сервисов

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
                //opt.Cookie.Expiration = TimeSpan.FromDays(10); - уже неподдерживается в 3.1
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                //L6 Identity Пути по которым система будет перенаправлять неавторизованных пользователей на контроллер Account
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true; //L6 ПШ Параметр который заставит систему автоматически подменять идентификатор сессии пользователя, как он авторизовался
            });

            //БД настройка инициализатора БД
            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //ПШ есть 3 возможных варианта регистрации сервисов 
            //ПШ м.AddTransient - каждый раз будет создаваться экземпляр сервиса
            //ПШ м.AddScoped - один экземпляр на область видимости
            //ПШ м.AddSingleton - один экземпляр на всё решение
            //ПШ создается один объект, который выдается всем подряд время жизни приложения, 
            //ПШ т.е. приложение запоминает и хранит объект на протяжении всей жизни процесса запущенного приложения, 
            //ПШ а значит хранит таблицу и модификации наших сотрудников

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SQLProductData>();

            services.AddScoped<ICartService, CookiesCartService>();
        }

        //ПШ в этом методе мы можем запросить все сервисы с которыми имеет дело наше приложение, в дальнейшем мы добавим инициализатор БД
        //ПШ конфигуриться конвейер, куда можно добавлять промежуточное ПО
        //ПШ для объекта app запускаются методы расширения, которые мы можем сами писать
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
            //5 ПШ инициализация БД
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            //ПШ описание валидации модели с помощью флюент интерфейса
            
            app.UseRouting();

            //L6 добавили Identity промежуточное ПО
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
