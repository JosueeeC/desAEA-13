using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Request;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailCustomController : ControllerBase
    {

        private readonly ProjectContext _projectContext;

        public DetailCustomController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }


        [HttpPost]
        public async Task<ActionResult<Detail>> InsertDetailCustom(Detail_Request_v2 request)
        {
            try
            {

                foreach (var item in request.Details)
                {
                    Detail detail = new Detail();
                    detail.Amount = item.Amount;
                    detail.Price = item.Price;
                    detail.Subtotal = item.Subtotal;
                    detail.ProductId = item.ProductId;
                    detail.InvoiceId = request.IdInvoice;

                    _projectContext.Details.Add(detail);
                    await _projectContext.SaveChangesAsync();

                   
                }

                return CreatedAtAction("InsertDetailCustom", new { id = request.IdInvoice }, request.Details);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
