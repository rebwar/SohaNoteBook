using System.Threading.Tasks;
using SohaNotebook.DataService.IRepository;

namespace SohaNotebook.DataService.IConfiguration
{
    public interface IUowRepository
    {
        public IUserRepository UserRepository { get;  }

        Task CompleteAsync();
    }
}