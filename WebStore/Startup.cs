using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.Models;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration; //��� ��������� � appsettings.json
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //���� ��������� �������, ����� ����� ������������

            services.AddMvc();
            //(options => options.Filters.Add(new Example_SimpleActionFilter())) ��� ���������� ������� �� ���� ������� ���� ������������

            //������ ����������� SQL:
            services.AddDbContext<DAL.WebStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //���������� �����������:
            //services.AddSingleton<IService, InMemoryService>(); ����� ����� ������� = ������� ����� ���������� ��������� 
            //services.AddScoped(); ����� ������� ����� http-������� (�� ����������/�������� ��������)
            //services.AddTransient(); - ����������� ��� ������ �������

            services.AddSingleton(typeof(IitemService<EmployeeViewModel>), typeof(InMemoryEmployeesService));
            services.AddSingleton(typeof(IitemService<BookViewModel>), typeof(InMemoryBooksService));
            services.AddSingleton<IProductData, InMemoryProductData>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //����� �����������, ��� � ��� ����� �������������� (������� � ���������)

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //����������� ����������� ��������
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //���������� ��-��������� (��� �����������/��� ������/��������)
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{Id?}");

                endpoints.MapControllerRoute("controller1", "{controller=Employee}/{action=Employees}/{Id?}");

                endpoints.MapControllerRoute("controller2", "{controller=Book}/{action=Books}/{Id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });

            //������ app.UseEndpoints � ���� ������ (�� ��� ����������� �������� ��������� �������� ��-���������)
            //app.UseMvcWithDefaultRoute();
            //����� ����, � services.AddMvc ����� ���� ��������� options => (options.EnableEndpointRouting = false)
        }
    }
}
