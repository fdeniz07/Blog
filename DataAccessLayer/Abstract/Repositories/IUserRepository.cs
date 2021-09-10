﻿using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}