using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlogWeb.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile imageFile, string folderName = "userImages");
        IDataResult<ImageDeletedDto> Delete(string imageName);
    }
}
