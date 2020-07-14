using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.Infrastructure.Services;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //�� ���� 3 ��������� �������� ����������� �������� 
            //�� �.AddTransient - ������ ��� ����� ����������� ��������� �������
            //�� �.AddScoped - ���� ��������� �� ������� ���������
            //�� ��������� ���� ������, ������� �������� ���� ������ ����� ����� ����������, 
            //�� �.�. ���������� ���������� � ������ ������ �� ���������� ���� ����� �������� ����������� ����������, 
            //�� � ������ ������ ������� � ����������� ����� �����������

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

        }

        //�� � ���� ������ �� ����� ��������� ��� ������� � �������� ����� ���� ���� ����������, � ���������� �� ������� ������������� ��
        //�� ������������� ��������, ���� ����� ��������� ������������� ��
        //�� ��� ������� app ����������� ������ ����������, ������� �� ����� ���� ������
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            //�� �������� ��������� ������ � ������� ������ ����������
            
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
