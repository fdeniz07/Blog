using DataAccessLayer.Abstract.Repositories;
using DataAccessLayer.Abstract.UnitOfWorks;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using DataAccessLayer.Concrete.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        //Kendi Context nesnemizi DI ile cagiriyoruz.
        private readonly MsDbContext _context;

        public UnitOfWork(MsDbContext context)
        {
            _context = context;
        }

        /*Yeni eklenen EfRepository lerimizi buraya eklememiz gerekli! Interface'ler new'lenemedigi icin repository'lerin somut hallerine ihtiyac duyulmaktadir.
         Bunu da, DependencyInjection(DI) ile yardimi ile yapiyoruz.
         */

        private EfAboutRepository _aboutRepository;
        private EfAuthorRepository _authorRepository;
        private EfBlogRepository _blogRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;
        private EfContactRepository _contactRepository;
        private EfUserRepository _userRepository;

        //Interface den implemente ettigimiz property ler lamda expression ile instance olusturulur (yok ise)

        // ?? (null-coalescing) operatörü sayesinde bir degiskenin degerinin null oldugu durumda alternatif deger döndürebiliriz.

        public IAboutRepository Abouts => _aboutRepository =_aboutRepository ?? new EfAboutRepository(_context);

        public IAuthorRepository Authors => _authorRepository ??= new EfAuthorRepository(_context);

        public IBlogRepository Blogs => _blogRepository ??= new EfBlogRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ??= new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ??= new EfCommentRepository(_context);

        public IContactRepository Contacts => _contactRepository ??= new EfContactRepository(_context);

        public IUserRepository Users => _userRepository ??=new EfUserRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
