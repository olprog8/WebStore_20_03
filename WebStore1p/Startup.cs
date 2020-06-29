using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
              services.AddControllersWithViews().AddRazorRuntimeCompilation();        }

        //в этом методе мы можем запросить все сервисы с которыми имеет дело наше приложение, в дальнейшем мы добавим инициализатор БД
        //конфигуриться конвейер, куда можно добавлять промежуточное ПО
        //для объекта app запускаются методы расширения, которые мы можем сами писать
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

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
