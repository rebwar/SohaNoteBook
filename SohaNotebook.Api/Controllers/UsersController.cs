using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SohaNotebook.DataService.Data;
using SohaNotebook.DataService.IConfiguration;
using SohaNotebook.Entities.DbSet;
using SohaNotebook.Entities.Dtos.Incoming;

namespace SohaNotebook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IUowRepository _unitOfWork;

        public UsersController(IUowRepository unitOfWork)//AppDbContext context)
        {
            _unitOfWork = unitOfWork;

            // _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Country = userDto.Country,
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    DateOfBirth = userDto.DateOfBirth,
                };
               await _unitOfWork.UserRepository.Add(user);
               await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
           // var users = _context.Users.Where(c => c.Status == 1).ToList();
           var users=await _unitOfWork.UserRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user =await _unitOfWork.UserRepository.Get(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
    }
}