using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlogWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)] // Bu attribute bir sinif üzerinden ya da metot üzerinde kullanilabilir
    public class ViewCountFilterAttribute:Attribute, IAsyncActionFilter
    {
        //Buranin amaci, kullanicilarin makaleleri okuma sayisinin cookie lere bagli kalarak arttirilmasini saglamak. Daha önce kullanici sayfayi yenilediginde okunma sayisi 1 artarak devam ediyor ve amatör bir sonuc ortaya cikariyordu.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            #region Aciklama

            /* 1- Gerekli action üzerinden blogId parametresinin alinmasini saglamak
             * 2- blogId alindiktan sonra bunun null olup olmadigini kontrol ediyoruz.
             * Kontrol sonucunda blogId null degilse;
             * 3- Cookieler arasinda ilglili blog+blognumarasi (blog22) seklinde okumaya calisiyoruz. Eger bizler bu makaleyi daha önce okumussak, cookiler arasina bu deger yazilmis olmalidir. Eger bu deger daha önce yazilmamissa if blogu icerisine giriyor;
             * 4- Gelen id cookielerde mevcut degilse, bu degeri cookie üzerine yaziyoruz. (blog+blogNumarasi,value olarak blogId, bitme süresi olarak 1 yil,geriye kalan response olarak da context üzerindeki response)
             * 5- Yukaridaki islem taraciyi üzerine bir cookie yazilmasini sagliyor. Bu islemden sonra BlogService kullanilarak, BlogService üzerindeki IncreaseViewCountAsync metodu calistirilarak okunma sayisi arttiriliyor.
             * 6- Await next() metodu ile View in yüklenmesini ve islemlerin devam etmesini sagliyoruz.
             *
             */

            #endregion
            
            var blogId = context.ActionArguments["blogId"];
            if (blogId is not null)
            {
                string blogValue = context.HttpContext.Request.Cookies[$"blog{blogId}"]; //blog10  blog3 blog21
                if (string.IsNullOrEmpty(blogValue))
                {
                    Set($"blog{blogId}",blogId.ToString(),1,context.HttpContext.Response);
                    var blogService = context.HttpContext.RequestServices.GetService<IBlogService>(); // bu ifadeyi yukariya eklemek gerekiyor. using Microsoft.Extensions.DependencyInjection;
                    await blogService.IncreaseViewCountAsync(Convert.ToInt32(blogId));
                    await next(); // await next() metodu kullanilmazsa yukaridaki view in yüklenmesini beklememis oluruz
                }
            }
            await next();
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime, HttpResponse response)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddYears(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMonths(6);

            response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }

    }
}
