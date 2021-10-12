using BlogWeb.Helpers.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using EntityLayer.ComplexTypes;

namespace BlogWeb.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string UserImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public async Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile imageFile,ImageType imageType,string folderName=null)
        {
            #region Özet
            /*
             * 1- Bize bu metot icerisinden bir folderName parametresi geliyor. Eger foldeName degiskeni bize null gelirse, parametreyle gelen imageType'i kontrol ediyoruz. Eger bu imageType user olarak gelirse userImagesFolder, post (makale)olarak gelmisse postImagesFolder olarak geriye döndürerek atamis olacagiz. Yani yeni bir resim eklerken, her seferinde yeni bir klasör adi vermemize gerek kalmiyor.
             * 2- Biz bu dosyanin daha önceden olup olmadigini kontrol ediyoruz. Eger yoksa bu dizin olusturuluyor, varsa ilgili islemler yapiliyor.
             * 3- Resmin adini oldFileName, resmin uzantisini da fileExtension degiskenlerine aliyoruz
             * 4- Bulunulan tarihi alip, bizim core katmanimizda daha önceden olusturdugumuz extension metodumuzda(FullDateAndTimeStringWithUnderscore) aldigimiz bilgileri formatliyoruz
             * 5- Yukaridaki adimdaki bilgileri kullanarak bir path olusturduk (wwwroot/img/yeniResim)
             * 6- Devaminda FileStream i kullanarak, gerekli path üzerine resmimizin kopyalanmasini sagladik
             * 7- Daha sonrasinda da bir DataResult 
             */
            #endregion

            /* Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (ImageType) klasör adı ataması yapılır. */
            folderName ??= imageType == ImageType.User ? UserImagesFolder : postImagesFolder;

            /* Eğer folderName değişkeni ile gelen klasör adı sistemimizde mevcut değilse, yeni bir klasör oluşturulur. */
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            /* Resimin yüklenme sırasındaki ilk adı oldFileName adlı değişkene atanır. */
            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName); // fatihdeniz

            /* Resmin uzantısı fileExtension adlı değişkene atanır. */
            string fileExtension = Path.GetExtension(imageFile.FileName); //.png

            Regex regex = new Regex("[*'\",+-._&#^@|/<>~]");
            //name = regex.Replace(name, string.Empty); // biz burada string.Empty ile regex degerlerinden gelen karakterleri resimden kaldirdik
            name = regex.Replace(name, "_");

           DateTime dateTime = DateTime.Now;

            /*
            // Parametre ile gelen değerler kullanılarak yeni bir resim adı oluşturulur.
            // Örn: FatihDeniz_601_5_38_12_28_09_2021.png
            //string fileName = $"{UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName2}";
            */
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            /* Kendi parametrelerimiz ile sistemimize uygun yeni bir dosya yolu (path) oluşturulur. */
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            /* Sistemimiz için oluşturulan yeni dosya yoluna resim kopyalanır. */
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            /* Resim tipine göre kullanıcı için bir mesaj oluşturulur. */
            string nameMessage = imageType == ImageType.User
                ? $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir."
                : $"{name} adlı makalenin resmi başarıyla yüklenmiştir.";
            
            return new DataResult<ImageUploadedDto>(ResultStatus.Success, nameMessage, new ImageUploadedDto
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = imageFile.Length
            }); // FatihDeniz_587_5_38_12_28_09_2021.png - "~/img/user.Image"
        }

        public IDataResult<ImageDeletedDto> Delete(string imageName)
        {
            // Amac : Kullanici resmini güncelledikten sonra eski resmin sunucudan silinmesi veyahut silinen bir kullanicinin resmininde silinmesi

            #region Özet
            /*
             * 1- Resim silinmeden önce bilgilerini fileToDelete degiskenine aliyoruz
             * 2- Sonrasinda bu resmin var olup olmadigini kontrol ediyoruz
             * 3- Resim bilgilerini imageDeletedDto aldiktan sonra silme islemini tamamlayip, bir  Success result dönüyoruz.
             * 4- Eger böyle bir resim yoksa Error result dönüyoruz
             */
            #endregion

            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", imageName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = imageName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error, $"Böyle bir resim bulunamadı.", null);
            }
        }
    }
}
