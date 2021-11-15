using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserDto>> GetAsync(int userId);
        Task<IDataResult<UserUpdateDto>> GetUserUpdateDtoAsync(int userId);
        Task<IResult> AddAsync(UserAddDto userAddDto);
        Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto);
        Task<IResult> DeleteAsync(int userId);
        Task<IResult> UndoDeleteAsync(int userId);
        Task<IResult> HardDeleteAsync(int userId);
    }
}
