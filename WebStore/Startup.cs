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
        private readonly IConfiguration _configuration; //для обращения к appsettings.json
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //сюда добавляем сервисы, какие будем использовать

            services.AddMvc();
            //(options => options.Filters.Add(new Example_SimpleActionFilter())) для добавления фильтра ко всем методам всех контроллеров

            //строка подключения SQL:
            services.AddDbContext<DAL.WebStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //Разрешение зависимости:
            //services.AddSingleton<IService, InMemoryService>(); время жизни сервиса = времени жизни запущенной программы 
            //services.AddScoped(); равно времени жизни http-запроса (до обновления/закрытия страницы)
            //services.AddTransient(); - обновляется при каждом запросе

            services.AddSingleton(typeof(IitemService<EmployeeViewModel>), typeof(InMemoryEmployeesService));
            services.AddSingleton(typeof(IitemService<BookViewModel>), typeof(InMemoryBooksService));
            services.AddSingleton<IProductData, InMemoryProductData>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //здесь прописываем, что и как будет использоваться (связано с сервисами)

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //подключение статических ресурсов
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //контроллер по-умолчанию (имя контроллера/имя метода/действие)
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{Id?}");

                endpoints.MapControllerRoute("controller1", "{controller=Employee}/{action=Employees}/{Id?}");

                endpoints.MapControllerRoute("controller2", "{controller=Book}/{action=Books}/{Id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });

            //аналог app.UseEndpoints в одну строку (но без возможности подробно настроить маршруты по-умолчанию)
            //app.UseMvcWithDefaultRoute();
            //кроме того, в services.AddMvc тогда надо прописать options => (options.EnableEndpointRouting = false)
        }
    }
}
