using GoodsPlan.Extensions;
using GoodsPlan.Infrastructure;
using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Infrastructure.Modules;
using GoodsPlan.Infrastructure.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace GoodsPlan
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Сбор данных о модулях
            GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;
            services.AddModules();

            // Подключение к базе данных
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            // Расширение поиска представлений
            services.Configure<RazorViewEngineOptions>(
                options => { options.ViewLocationExpanders.Add(new ViewLocationExpander()); });

            services.AddRazorPages();

            services.AddSwaggerGen();

            // Добавление авторизации
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/login");
                });

            // Внедрение записимости класса для работы с БД
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Внедрение зависимостей из модулей
            foreach (var module in GlobalConfiguration.Modules)
            {
                var moduleInitializerType = module.Assembly.GetTypes()
                   .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
                if ((moduleInitializerType != null) && (moduleInitializerType != typeof(IModuleInitializer)))
                {
                    var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                    services.AddSingleton(typeof(IModuleInitializer), moduleInitializer);
                    moduleInitializer.ConfigureServices(services);
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
