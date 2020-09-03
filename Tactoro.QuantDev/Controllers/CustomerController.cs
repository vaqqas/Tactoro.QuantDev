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
    public class CustomerController : ControllerBase
    {
        private readonly QuantDevDBContext _context;

        public CustomerController(QuantDevDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetCustomers()
        {
            return GetPoco(await _context.Customers.ToListAsync());
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfo>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return new CustomerInfo(customer);
        }

        // GET: api/Customer/5
        [Route("GetCustomersWithManagers")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CustomerWithManagerInfo>>> GetCustomersWithManagers()
        {
            var customers = await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Manager)
                .Include(c => c.Manager.User)
                .ToListAsync();

            var allCustomerWithManagersInfo = new List<CustomerWithManagerInfo>();
            foreach (var customer in customers)
            {
                allCustomerWithManagersInfo.Add(new CustomerWithManagerInfo(customer));
            }
            return allCustomerWithManagersInfo;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerInfo customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerInfo>> PostCustomer(CustomerInfo customer)
        {
            _context.Customers.Add(customer.CreateModel());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerInfo>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new CustomerInfo(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private List<CustomerInfo> GetPoco(IEnumerable<Customer> customers)
        {
            var allCustomerInfo = new List<CustomerInfo>();
            foreach (var customer in customers)
            {
                allCustomerInfo.Add(new CustomerInfo(customer));
            }
            return allCustomerInfo;
        }
    }
}
