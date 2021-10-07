using AutoMapper;
using DataAccessLayer.Abstract.UnitOfWorks;

namespace BusinessLayer.Concrete
{
    public class ManagerBase
    {
        public ManagerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected IMapper Mapper { get; }
    }
}
