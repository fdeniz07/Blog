using System.Text.Json.Serialization;
using AutoMapper;
using BlogWeb.AutoMapper.Profiles;
using BlogWeb.Filters;
using BlogWeb.Helpers.Abstract;
using BlogWeb.Helpers.Concrete;
using BusinessLayer.AutoMapper.Profiles;
using BusinessLayer.Concrete;
using BusinessLayer.Extensions;
using CoreLayer.Utilities.Extensions;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

            //FluentValidation dogrulamalari icin buraya servis ekliyoruz. RegisterValidatorsFromAssemblyContaining icin ilgili katmandaki herhangi bir class adi verilir, program calistiginda ilgili Validation lar assembly üzerinde taranir. Bu sayede tek tek class'lar buraya yazilmamis olur.
            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<UserManager>();
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            //UI katmanina 2 paket yüklenmeli
            /*
             * 1-AutoMapper.Extensions.Microsoft.DependencyInjection
             * 2-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
             */

            //services.AddTransient< IImageHelper, ImageHelper>(); //Transient kavrami herbir islem icin yeni bir ImageHelper olusturmus oluyoruz

            /* AddSingleton Mapper Ne ise yarar?
             *
             * Burada olusturacagimiz mapper, bizler icin provider üzerinden gerekli servislerin alinmasini ve servislerimize gecilmesini saglar ve buradaki islemler harmanlandiktan sonra da bizlere yeni bir mapper olarak return edilir.
             */

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                // Biz bir profil eklemek istiyoruz, ve bu profil bizden bir ImageHelper bekledigi icin de, provider üzerinden GetService diyerek IImageHelper'i veriyoruz. Bu sayede buradaki islem gerekli profilin gerekli servis ile olusturulmasini saglar.
                //cfg.AddProfile(new UserProfile(provider.GetService<IImageHelper>())); // Resim yükleme II.Yolu kullansaydik burada servis gecerdik
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new CategoryProfile());
                cfg.AddProfile(new BlogProfile());
                cfg.AddProfile(new ViewModelsProfile());
                cfg.AddProfile(new CommentProfile());
            }).CreateMapper());

            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.Configure<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<BlogRightSideBarWidgetOptions>(Configuration.GetSection("BlogRightSideBarWidgetOptions"));
           
            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.ConfigureWritable<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<BlogRightSideBarWidgetOptions>(Configuration.GetSection("BlogRightSideBarWidgetOptions"));

            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boş geçilmemelidir."); //Ingilzce hata mesajlari yerine, hatali dönüsler icin bizim belirledigimiz mesaj gösterilecektir.
                options.Filters.Add<MvcExceptionFilter>(); // Global hata yönetimimizi buraya ekliyoruz.
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();//Bu sayede backend de yapilan degisiklerde tekrar tekrar uygulamayi derlememize ihtiyac kalmiyor. Yani frontend deki gibi kaydettikten sonra uygulamadaki degisiklikleri görebiliriz.

            services.AddSession();
           /* services.AddAutoMapper(typeof(CategoryProfile), typeof(BlogProfile), typeof(UserProfile), typeof(ViewModelsProfile), typeof(CommentProfile));*/ //Derlenme sirasinda Automapper in buradaki siniflari taramasi saglaniyor. --> En yukarida AutoMapper'i singleton olarak tanimliyoruz ve bu kismi siliyoruz.

            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalDB")); // Daha önceden kurdugumuz yapiyi buradan yüklüyoruz

            services.AddScoped<IImageHelper, ImageHelper>(); //Resim yükleme II.Yol olarak kullanmak istersek, bu kismi en yukariya transient olarak yaziyoruz

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "Blog",
                    HttpOnly = true,
                    SameSite =SameSiteMode.Strict, // Siteler arasi istek sahtekarligina (Cross Site Request Forgery - CSRF/XSRF - Session Riding) karsi önlem. Kaynak : https://www.prismacsi.com/cross-site-request-forgery-csrf-nedir/
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //Site canliya tasindiginda bu alan .Always olarak degistirilmelidir.!!!
                };
                options.SlidingExpiration = true; //Cookie süresi belirleme
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // 7 gün boyunca tarayici üzerinde gecerliligi olacak
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //Yetkisiz erisimde yönlendirilecek sayfa
            });
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
            app.UseNToastNotify();
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
                endpoints.MapControllerRoute(
                    name:"blog",
                    pattern:"{title}/{blogId}", //"{categoryName}/{title}/{blogId}",
                    defaults:new {controller="Blog",action="Detail"}
                    );
                endpoints.MapDefaultControllerRoute(); // Bu islem varsayilan olarak, sitemiz acildigindan default olarak HomeController ve Index kismina gider
            });
        }
    }
}
