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
