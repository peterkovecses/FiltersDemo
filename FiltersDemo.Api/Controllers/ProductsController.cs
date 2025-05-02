namespace FiltersDemo.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductRepository productRepository) : Controller
{
    [LogResponseSize]
    [CustomResponseCache(30)]
    [HttpGet]
    public async Task<ActionResult<List<Product>>>GetProducts()
    {
        return await productRepository.GetProductsAsync();
    }
    
    [NotFoundIfNull]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product?>> GetProduct(int id)
    {
        var product = await productRepository.GetProductAsync(id);
            
        return product;
    }
    
    [ApiKeyAuthorization]
    [ProductPriceValidation]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductRequest request)
    {
        var product = await productRepository.CreateProductAsync(request);
        
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
    
    [HttpDelete("{id:int}")]
    public Task<ActionResult> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}