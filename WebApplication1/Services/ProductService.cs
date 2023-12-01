using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductService
    {
        private readonly ProjectContext _projectContext;

        public ProductService(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public List<Product> GetProducts()
        {
            try
            {

                var products = _projectContext.Products.Where(x => x.Enabled == true).ToList();

                return products;
            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }

        public async void InsertProduct(Product product)
        {
            _projectContext.Add(product);
            await _projectContext.SaveChangesAsync();
        }

        public Product GetProductById(int id)
        {
            
             Product response = _projectContext.Products.FirstOrDefault(x => x.ProductId == id && x.Enabled == true);

            return response;
        }

        public async void DeleteProduct(int id)
        {
            Product response = _projectContext.Products.FirstOrDefault(x => x.ProductId == id );
            response.Enabled = false;
            await _projectContext.SaveChangesAsync();
        }
    }
}
