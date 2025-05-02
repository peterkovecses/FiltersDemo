namespace FiltersDemo.Api.Data;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> Products = 
    [
        new(1, "Laptop", 999.99m),
        new(2, "Smartphone", 599.99m),
        new(3, "Headphones", 149.99m),
        new(4, "Monitor", 299.99m),
        new(5, "Keyboard", 79.99m),
        new(6, "Mouse", 49.99m),
        new(7, "Printer", 199.99m),
        new(8, "Tablet", 399.99m),
        new(9, "Camera", 449.99m),
        new(10, "Speaker", 129.99m)
    ];

    public Task<List<Product>> GetProductsAsync()
        => Task.FromResult(Products);

    public Task<Product?> GetProductAsync(int id)
        => Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
    
    public Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product(Products.Max(product => product.Id) + 1, request.Name, request.Price);
        Products.Add(product);
        
        return Task.FromResult(product);
    }
}