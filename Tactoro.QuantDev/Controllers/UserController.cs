using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tactoro.QuantDev.Models;

namespace Tactoro.QuantDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QuantDevDBContext _context;

        public UserController(QuantDevDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUser()
        {
            return GetPoco(await _context.Users.ToListAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserInfo(user);
        }

        // GET: api/Manager/5
        [Route("Query")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserInfo>>> Query(int? id = null, string userName = null, string email = null, string alias = null, string firstName = null, string lastName = null)
        {
            var users = await _context.Users.Where(u =>
                    (id == null || u.Id == id)
                && (userName == null || u.UserName.ToLower() == userName.ToLower())
                && (email == null || u.Email.ToLower() == email.ToLower())
                && (firstName == null || u.FirstName.ToLower() == firstName.ToLower())
                && (lastName == null || u.FirstName.ToLower() == lastName.ToLower())
                && (alias == null || u.Alias.ToLower() == alias.ToLower())).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return GetPoco(users);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserInfo user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user.CreateModel()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserInfo user)
        {
            _context.Users.Add(user.CreateModel());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInfo>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new UserInfo(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private List<UserInfo> GetPoco(IEnumerable<User> users)
        {
            var allUsersInfo = new List<UserInfo>();
            foreach (var user in users)
            {
                allUsersInfo.Add(new UserInfo(user));
            }
            return allUsersInfo;
        }
    }
}
