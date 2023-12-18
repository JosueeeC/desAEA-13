using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Request;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCustomController : ControllerBase
    {
        private readonly ProjectContext _projectContext;

        public ProductCustomController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> InsertProduct(Product_Request_v1 request)
        {
            try
            {
                Product product = new Product();

                product.Name = request.Name;
                product.Price = request.Price;
                product.Enabled = true;

                _projectContext.Products.Add(product);
                await _projectContext.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);

            }
            catch (Exception ex) 
            { 
            
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductCustom(Product_Request_v2 request)
        {


            var product = await _projectContext.Products.FindAsync(request.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Enabled = false;
            await _projectContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> UpdatePriceProduct(Product_Request_v3 request)
        {
            try
            {
                var product = await _projectContext.Products.FindAsync(request.Id);
                product.Price = request.Price;

                await _projectContext.SaveChangesAsync();

                return CreatedAtAction("UpdatePriceProduct", new { id = product.ProductId }, product);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> DeleteListProduct(Product_Request_v4 request)
        {
            try
            {

                foreach(var item in request.ProductId)
                {
                    var res = _projectContext.Products.FirstOrDefault(x => x.ProductId == item);

                    res.Enabled = false;

                    await _projectContext.SaveChangesAsync();

                }

                return CreatedAtAction("DeleteListProduct", request);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
