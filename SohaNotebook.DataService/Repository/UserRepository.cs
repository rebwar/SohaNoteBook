using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SohaNotebook.DataService.Data;
using SohaNotebook.DataService.IRepository;
using SohaNotebook.Entities.DbSet;

namespace SohaNotebook.DataService.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public async override Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users.Where(c => c.Status == 1)
                            .AsNoTracking()
                            .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} All Method has generated an error!", typeof(UserRepository));
                return new List<User>();
            }
        }
    }
}