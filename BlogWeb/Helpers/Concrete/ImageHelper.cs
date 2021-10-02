using BlogWeb.Helpers.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;

namespace BlogWeb.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile imageFile, string folderName="userImages")
        {
            // ~/img/user.Picture

            #region Özet
            /*
             * 1- Bize bu metot icerisinden bir folderName parametresi geliyor
             * 2- Biz bu dosyanin daha önceden olup olmadigini kontrol ediyoruz. Eger yoksa bu dizin olusturuluyor, varsa ilgili islemler yapiliyor.
             * 3- Resmin adini oldFileName, resmin uzantisini da fileExtension degiskenlerine aliyoruz
             * 4- Bulunulan tarihi alip, bizim core katmanimizda daha önceden olusturdugumuz extension metodumuzda(FullDateAndTimeStringWithUnderscore) aldigimiz bilgileri formatliyoruz
             * 5- Yukaridaki adimdaki bilgileri kullanarak bir path olusturduk (wwwroot/img/yeniResim)
             * 6- Devaminda FileStream i kullanarak, gerekli path üzerine resmimizin kopyalanmasini sagladik
             * 7- Daha sonrasinda da bir DataResult 
             */
            #endregion

            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName); // fatihdeniz
            string fileExtension = Path.GetExtension(imageFile.FileName); //.png
            DateTime dateTime = DateTime.Now;
            //FatihDeniz_601_5_38_12_28_09_2021_userFatihDenizResmi.png
            //string fileName = $"{UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName2}";

            //FatihDeniz_601_5_38_12_28_09_2021.png
            string newFileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return new DataResult<ImageUploadedDto>(ResultStatus.Success,$"{userName} adlı kullanıcının resmi başarıyla yüklenmiştir.",new ImageUploadedDto
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
