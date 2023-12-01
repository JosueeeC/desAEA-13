using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCustomController : ControllerBase
    {
        private readonly ProjectContext _projectContext;
        public InvoiceCustomController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }


        [HttpPost]
        public List<Invoice> GetByFilter(string request)
        {
            var response = _projectContext.Invoices.Include(x=> x.Customer.FirstName.Contains(request)).ToList();

            return response;
        }
    }
}
