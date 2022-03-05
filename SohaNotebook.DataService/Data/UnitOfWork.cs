using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SohaNotebook.DataService.IConfiguration;
using SohaNotebook.DataService.IRepository;
using SohaNotebook.DataService.Repository;

namespace SohaNotebook.DataService.Data
{
    public class UnitOfWork : IUowRepository, IDisposable
    {
        public IUserRepository UserRepository { get; private set; }
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DBlogger");
            UserRepository = new UserRepository(context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}