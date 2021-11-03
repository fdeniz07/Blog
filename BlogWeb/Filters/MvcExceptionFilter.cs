using System;
using System.Data.SqlTypes;
using CoreLayer.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;

namespace BlogWeb.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;

        public MvcExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
        }

        public void OnException(ExceptionContext context)
        {

            #region Bu filtrenin görevi nedir?

            /*
            * Sunucuya bir istek yapildiginda ve uygulamada bu request islenirken, eger exception'a düserse
              bu noktadan itibaren MvcExceptionFilter devreye girecek.
            *
            * Bu noktadan itibaren de yazilan kod satirlari islemeye baslayacaktir.
            *
            * Bir if kontrolü ekliyor ve ortamini belirtiyoruz. Eger uygulama henüz gelistirme ortamindaysa, isDevelopment, uygulama canli ortama tasindi ise IsProduction olarak yapilir.
            *
            * Normalde hata sayfalari kullaniciya bos bir beyaz sayfa olarak gelir. Biz bunu istemiyoruz. Bu noktadan itibaren kendi sayfamizi kullaniciya return ediyoruz.
            *
            * Hata kismini aktif ediyoruz (context.ExceptionHandled = true;) - Hata bizim tarafimizdan kontrol altina alindigi icin
            *
            * Bir model olusturuyor ve mesajlarimizi hata türlerine göre switch-case yapisi ile giriyoruz.
            *
            * Bir ViewResult dönecegimiz icin, bir üst adimdaki modelimizi olusturuyoruz.
            *
            * Geri dönecek result degerini bizim olusturdugumuz result ile degistiriyor (yani bizim olusturdugumuz hata sayfamiz oluyor.) ve bu View'e bir isim atiyor ve bu isimde bir view sayfasi olusturmamiz gerekiyor({ ViewName = "Error" };)
            *
            * Hata codumuzu 500 olarak ic server hata kodu olarak belirliyoruz. Hata kodunu belirlemek bizlere kalmis bir olay.
            *
            * ViewData olarak bizim suan ki ModelState'i (context üzerinden gelen) eklememiz gerekiyor.
            *
            * Son olarak da, ViewResult'da kullanmak istedigimiz modeli eklemek icinde ViewData.Add olarak bir isim ekliyoruz.
            *
            * Result degerini, bizim olusturdugumuz result degeri ile degistirip, hata yönetimini global olarak ele almis oluyoruz.
            *
            * Geriye kalan son adim olarak, burada olusturulan filtrenin uygulamaya eklenmesi adimi kaldi. Startup.cs icerisinde gerekli eklemeler yapilir.
            */

            #endregion

            //Asagidaki alan gelistirme esnasindayken _environment.IsDevelopment() olmali. Canliya tasindigi andan itibaren _environment.IsProduction() olarak yapilmalidir!
            if (_environment.IsDevelopment())
            {
                context.ExceptionHandled = true;
                var mvcErrorModel = new MvcErrorModel();
                ViewResult result;
                switch (context.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir veritabanı hatası oluştu. Sorunu en kısa sürede çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;
                        break;
                    case NullReferenceException:
                        mvcErrorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir null veriye rastlandı. Sorunu en kısa sürede çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 403;
                        break;
                    default:
                        mvcErrorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir hata oluştu. Sorunu en kısa sürede çözeceğiz.";
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;
                        break;
                }
                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("MvcErrorModel", mvcErrorModel);
                context.Result = result;
            }
        }
    }
}
