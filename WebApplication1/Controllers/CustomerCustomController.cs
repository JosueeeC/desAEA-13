using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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

    }
}
