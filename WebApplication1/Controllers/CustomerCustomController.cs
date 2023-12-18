using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Request;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerCustomController : ControllerBase
    {
        private readonly ProjectContext _projectContext;
        public CustomerCustomController(ProjectContext projectContext) {
            _projectContext = projectContext;        
        }

        [HttpPost]
        public List<Customer> GetByFilter(Customer request)
        {
            var response = _projectContext.Customers.Where(x=> x.FirstName.Contains(request.FirstName)  || x.LastName.Contains(request.LastName) ).ToList();

            return response;
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> InsertCustomer (Customer_Request_v1 request)
        {
            try
            {
                Customer customer = new Customer();
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.DocumentNumber = request.DocumentNumber;
                customer.Active = true;
                

                _projectContext.Customers.Add(customer);
                await _projectContext.SaveChangesAsync();

                return CreatedAtAction("InsertCustomer", new { id = customer.CustomerId }, customer);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomerCustom(Customer_Request_v2 request)
        {


            var customer = await _projectContext.Customers.FindAsync(request.CustomerId);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Active = false;
            await _projectContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCustomer (Customer_Request_v3 request)
        {
            try
            {

                var customer = await _projectContext.Customers.FindAsync(request.CustomerId);
                customer.DocumentNumber = request.DocumentNumber.ToString();

                await _projectContext.SaveChangesAsync();


                return CreatedAtAction("InsertCustomer", new { id = customer.CustomerId }, customer);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertListInvoices(Invoice_Request_v4 request)
        {
            try
            {

                var customer = await _projectContext.Customers.FindAsync(request.CustomerId);
                foreach(var invoice in request.Invoice)
                {

                    invoice.CustomerId = request.CustomerId;

                    _projectContext.Customers.Add(customer);
                    await _projectContext.SaveChangesAsync();

                }

                

                return CreatedAtAction("InsertListInvoices", new { id = customer.CustomerId }, customer);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
