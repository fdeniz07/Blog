using System.Text.Json.Serialization;
using BusinessLayer.AutoMapper.Profiles;
using BusinessLayer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //UI katmanina 2 paket yüklenmeli
            /*
             * 1-AutoMapper.Extensions.Microsoft.DependencyInjection
             * 2-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
             */

            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.Preserve;
            });//Bu sayede backend de yapilan degisiklerde tekrar tekrar uygulamayi derlememize ihtiyac kalmiyor. Yani frontend deki gibi kaydettikten sonra uygulamadaki degisiklikleri görebiliriz.

            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(BlogProfile)); //Derlenme sirasinda Automapper in buradaki siniflari taramasi saglaniyor.
            services.LoadMyServices(); // Daha önceden kurdugumuz yapiyi buradan yüklüyoruz
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); // Sayfa bulunmadiginda 404 hata sayfasina yönlendirecektir
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles(); // static dosyalarimiz: resim,css,js vb
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                //Routing islemlerimiz:
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute(); // Bu islem varsayilan olarak, sitemiz acildigindan default olarak HomeController ve Index kismina gider
            });
        }
    }
}
