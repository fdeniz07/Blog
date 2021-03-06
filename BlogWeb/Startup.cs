using AutoMapper;
using BlogWeb.AutoMapper.Profiles;
using BlogWeb.Filters;
using BlogWeb.Helpers.Abstract;
using BlogWeb.Helpers.Concrete;
using BusinessLayer.AutoMapper.Profiles;
using BusinessLayer.Concrete;
using BusinessLayer.Extensions;
using CoreLayer.Utilities.Extensions;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smidge;
using System.Text.Json.Serialization;

namespace BlogWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; //Bu sayede appsettings.json dosyasindaki ayarlara erisebiliriz
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Smidge Library
            services.AddSmidge(Configuration.GetSection("smidge")); //appsettings.json dan ayari okuyacagimiz bir isim veriyoruz.


            //FluentValidation dogrulamalari icin buraya servis ekliyoruz. RegisterValidatorsFromAssemblyContaining icin ilgili katmandaki herhangi bir class adi verilir, program calistiginda ilgili Validation lar assembly üzerinde taranir. Bu sayede tek tek class'lar buraya yazilmamis olur.
            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<UserManager>(); //Buradaki validations BLL katmaninda
                options.RegisterValidatorsFromAssemblyContaining<Startup>(); //Buradaki validations UI katmaninda
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
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();//Bu sayede backend de yapilan degisiklerde tekrar tekrar uygulamayi derlememize ihtiyac kalmiyor. Yani frontend deki gibi kaydettikten sonra uygulamadaki degisiklikleri görebiliriz.

            services.AddSession();
            /* services.AddAutoMapper(typeof(CategoryProfile), typeof(BlogProfile), typeof(UserProfile), typeof(ViewModelsProfile), typeof(CommentProfile));*/ //Derlenme sirasinda Automapper in buradaki siniflari taramasi saglaniyor. --> En yukarida AutoMapper'i singleton olarak tanimliyoruz ve bu kismi siliyoruz.

            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB")); // Daha önceden kurdugumuz yapiyi buradan yüklüyoruz (identity,)
            services.AddScoped<IImageHelper, ImageHelper>(); //Resim yükleme II.Yol olarak kullanmak istersek, bu kismi en yukariya transient olarak yaziyoruz
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login"); //Kullanici üye olmadan, üye olmayanlarin erisebildigi sayfaya tiklarsa, Login sayfamiza yönlendirir
                options.LogoutPath = new PathString("/Admin/Auth/Logout"); //
                options.Cookie = new CookieBuilder
                {
                    Name = "Blog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, // Lax: default olarak gelir. Anasitemiz üzerinden subdomaine ait bir sitemize tek bir cookie ile gecilmesine olanak verir.
                    //Strict : Bu mode mali,finansal uygulamalar icin secilir ve Siteler arasi istek sahtekarligina (Cross Site Request Forgery - CSRF/XSRF - Session Riding) karsi önlem olarak kullanilir. Bu sayede Client - Sunucu arasinda cookie'ye müdahale edilmesine izin vermez. Subdomanin yapisinda kullanilmaz. Kaynak : https://www.prismacsi.com/cross-site-request-forgery-csrf-nedir/
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //Site canliya tasindiginda bu alan .Always olarak degistirilmelidir.!!!
                    //Always : Browser, kullanicinin cookie'sini sadece  Https üzerinden bir istek geldiginde gönderir
                    //SameAsRequest : Browser, kullanicinin cookie istegini hangi protokolden geldiyse ona o sekilde gönderir (Http den geldiyse http den, https den geldiyse https den)
                    //None: Browser, protokole bakmadan tüm gelen isteklere http üzerinden gönderir
                };
                options.SlidingExpiration = true; // Expiration süresi bitmedigi sürece kullanici hangi gün tekrar siteyi ziyaret ederse, Expiration süresi kadar üzerine süre eklenir
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
                app.UseStatusCodePages();  // Gelistirme ortamindayken, Sayfa bulunmadiginda 404 hata sayfasina yönlendirecektir
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession(); // Authentication ve Authorization dan önce gelmeli
            app.UseStaticFiles(); // static dosyalarimiz: resim,css,js vb
            app.UseRouting(); // Authentication ve Authorization dan önce gelmeli
            app.UseAuthentication(); //Authorization dan önce gelmeli
            app.UseAuthorization();
            app.UseNToastNotify();

            //Smidge kullanimi - EndPoints kisminin üzerinden cagrilabilir
            app.UseSmidge(bundle =>
            {
                //Smidge - JS \\

                //Admin
                bundle.CreateJs("admin-js-bundle", "~/AdminLTE/js/scripts.js", "~/AdminLTE/js/site.js", "~/AdminLTE/assets/demo/datatables-demo.js"); //dosyalar tek tek yazilacagi("~/js/site.js", "~/js/site2.js") gibi ortak klasör yolu da verilebilir

                bundle.CreateJs("admin-validationscriptspartial-js-bundle", "~/AdminLTE/lib/jquery-validation/dist/jquery.validate.min.js", "~/AdminLTE/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js");

                //Blog
                bundle.CreateJs("blog-contactlayout-js-bundle", "~/BlogHome/vendor/jquery/jquery.min.js", "~/BlogHome/vendor/bootstrap/js/bootstrap.bundle.min.js");

                bundle.CreateJs("blog-layoutjspartial-js-bundle", "~/BlogHome/js/bootstrap.js", "~/BlogHome/js/blogDetail.js", "~/BlogHome/js/jquery.desoslide.js", "~/BlogHome/js/move-top.js", "~/BlogHome/js/easing.js", "~/BlogHome/js/jquery.flexisel.js");


                // Smidge - CSS \\

                //Admin
                bundle.CreateCss("admin-userloginlayout-css-bundle", "~/AdminLTE/css/styles.css", "~/AdminLTE/css/userLogin.css");


                //Blog
                bundle.CreateCss("blog-layoutcsspartial-css-bundle", "~/BlogHome/css/bootstrap.css", "~/BlogHome/css/jquery.desoslide.css", "~/BlogHome/css/style.css", "~/BlogHome/css/fontawesome-all.css", "~/BlogHome/css/site.css");

                bundle.CreateCss("blog-contactlayoutcsspartial-css-bundle", "~/BlogHome/css/bootstrap.css", "~/BlogHome/css/style.css", "~/BlogHome/css/fontawesome-all.css", "~/BlogHome/css/contact-page.css");

                bundle.CreateCss("blog-errorlayout-css-bundle", "~/BlogHome/css/bootstrap.css", "~/BlogHome/css/error-page-bg-animation.css");

            });

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
                //Eger bizim baska area alanimiz varsa yukaridaki pattern :  "{area=User}/{controller=Home}/{action=Index}/{id?}" gibi


                endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "{title}/{blogId}", //"{categoryName}/{title}/{blogId}",
                    defaults: new { controller = "Blog", action = "Detail" }
                    );

                endpoints.MapDefaultControllerRoute(); // Bu islem varsayilan olarak, sitemiz acildigindan default olarak HomeController ve Index kismina gider
            });
        }
    }
}
