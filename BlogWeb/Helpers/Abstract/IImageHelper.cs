using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes;

namespace BlogWeb.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null); // I.Yol
        /*string Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null);*/ // II.Yol

        IDataResult<ImageDeletedDto> Delete(string imageName);
    }
}
