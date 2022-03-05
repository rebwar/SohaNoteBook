using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SohaNotebook.DataService.Data;
using SohaNotebook.Entities.DbSet;
using SohaNotebook.Entities.Dtos.Incoming;

namespace SohaNotebook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto userDto)
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
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.Where(c => c.Status == 1).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
    }
}