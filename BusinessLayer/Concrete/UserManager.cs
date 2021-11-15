using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Utilities;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Concrete
{
    public class UserManager:ManagerBase, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserManager(IUnitOfWork unitOfWork,IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }


        /////////////////////// GetAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<UserDto>> GetAsync(int userId)
        {
            throw new NotImplementedException();
        }


        /////////////////////// GetUserUpdateDtoAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\
        
        public async Task<IDataResult<UserUpdateDto>> GetUserUpdateDtoAsync(int userId)
        {
            throw new NotImplementedException();
        }


        /////////////////////// AddAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> AddAsync(UserAddDto userAddDto)
        {
            var user = Mapper.Map<User>(userAddDto);
            await UnitOfWork.Users.AddAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.User.Add(user.UserName));
        }


        /////////////////////// UpdateAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var oldUser = await UnitOfWork.Users.GetAsync(b => b.Id == userUpdateDto.Id);
            var user = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Blog.Update(user.UserName));
        }


        /////////////////////// DeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> DeleteAsync(int userId)
        {
            var result = await UnitOfWork.Users.AnyAsync(u => u.Id == userId);
            if (result)
            {
                var user = await UnitOfWork.Users.GetAsync(u => u.Id == userId);
                user.LockoutEnabled = true;
                await UnitOfWork.Users.UpdateAsync(user);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.User.Delete(user.UserName));
            }
            return new Result(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false));
        }


        /////////////////////// UndoDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\ Silinmisleri Geri Al

        public async Task<IResult> UndoDeleteAsync(int userId)
        {
            var result = await UnitOfWork.Users.AnyAsync(u => u.Id == userId);
            if (result)
            {
                var user = await UnitOfWork.Users.GetAsync(u => u.Id == userId);
                user.LockoutEnabled = false;
                user.LockoutEnd = DateTime.Now;
                await UnitOfWork.Users.UpdateAsync(user);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.User.UndoDelete(user.UserName));
            }
            return new Result(ResultStatus.Error, Messages.User.NotFound(isPlural: false));
        }


        /////////////////////// HardDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> HardDeleteAsync(int userId)
        {
            var result = await UnitOfWork.Users.AnyAsync(u => u.Id == userId);
            if (result)
            {
                var user = await UnitOfWork.Users.GetAsync(u => u.Id == userId);
                await UnitOfWork.Users.DeleteAsync(user);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.User.HardDelete(user.UserName));
            }
            return new Result(ResultStatus.Error, Messages.User.NotFound(isPlural: false));
        }
    }
}
