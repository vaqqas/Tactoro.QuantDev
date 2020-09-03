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
    public class ManagerController : ControllerBase
    {
        private readonly QuantDevDBContext _context;

        public ManagerController(QuantDevDBContext context)
        {
            _context = context;
        }

        // GET: api/Manager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerInfo>>> GetManagers()
        {
            return GetPoco(await _context.Managers.ToListAsync());
        }

        // GET: api/Manager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerInfo>> GetManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager == null)
            {
                return NotFound();
            }

            return new ManagerInfo(manager);
        }

        [Route("ManagerWithClients")]
        [HttpGet()]
        public async Task<ActionResult<ManagerWithClientsInfo>> GetManagerWithClients(string userName)
        {
            Manager manager = await _context.Managers
                .Where(m => m.User.UserName.ToLower() == userName.ToLower())
                .Include(m => m.User)
                .Include(m => m.Customers)
                .FirstOrDefaultAsync();

            if (manager == null)
            {
                return NotFound();
            }

            return new ManagerWithClientsInfo(manager);
        }

        [Route("GetAllManagersWithClients")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ManagerWithClientsInfo>>> GetAllManagersWithClients()
        {
            var managers = await _context.Managers
                .Include(m => m.User)
                .Include(m => m.Customers).ToListAsync();

            var allManagersWithCustomers = new List<ManagerWithClientsInfo>();
            foreach (var manager in managers)
            {
                allManagersWithCustomers.Add(new ManagerWithClientsInfo(manager));
            }
            return allManagersWithCustomers;
        }

        // GET: api/Manager/5
        [Route("Query")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ManagerInfo>>> Query(int? managerId = null, int? userId = null, string userName = null, string email = null, string alias = null, string firstName = null, string lastName = null)
        {
            var manager = await _context.Managers.Where(m =>
                    (managerId == null || m.Id == managerId)
                && (userId == null || m.User.Id == userId)
                && (userName == null || m.User.UserName.ToLower() == userName.ToLower())
                && (email == null || m.User.Email.ToLower() == email.ToLower())
                && (firstName == null || m.User.FirstName.ToLower() == firstName.ToLower())
                && (lastName == null || m.User.FirstName.ToLower() == lastName.ToLower())
                && (alias == null || m.User.Alias.ToLower() == alias.ToLower())).ToListAsync();

            if (manager == null)
            {
                return NotFound();
            }

            return GetPoco(manager);
        }

        // PUT: api/Manager/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManager(int id, ManagerInfo managerInfo)
        {
            if (id != managerInfo.ManagerId)
            {
                return BadRequest();
            }

            Manager manager = managerInfo.CreateModel();

            _context.Entry(manager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
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

        // POST: api/Manager
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ManagerInfo>> PostManager(ManagerInfo managerInfo)
        {
            var manager = managerInfo.CreateModel();
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            managerInfo.ManagerId = manager.Id;
            managerInfo.UserId = manager.UserId;

            return CreatedAtAction("PostManager", new { id = managerInfo.ManagerId }, managerInfo);
        }

        // DELETE: api/Manager/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ManagerInfo>> DeleteManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();

            return new ManagerInfo(manager);
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.Id == id);
        }

        private List<ManagerInfo> GetPoco(IEnumerable<Manager> managers)
        {
            var allManagersInfo = new List<ManagerInfo>();
            foreach (var manager in managers)
            {
                allManagersInfo.Add(new ManagerInfo(manager));
            }
            return allManagersInfo;
        }
    }
}
