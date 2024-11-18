using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all products
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    // Get product by Id
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    // Create a new product (without saving changes)
    public async Task<Product> AddProduct(ProductEditDto model)
    {
        var categoryRef = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == model.CategoryId);

        if (categoryRef == null)
        {
            throw new Exception("Category not found");
        }

        var product = new Product
        {
            Name = model.Name,
            Price = model.Price,
            CategoryId = model.CategoryId,
            Stock = model.Stock
        };

        _context.Products.Add(product);
        return product;
    }

    // Update a product (without saving changes)
    public Product UpdateProduct(ProductEditDto product)
    {
        var existingProduct = _context.Products.Find(product.Id);
        if (existingProduct == null)
        {
            throw new Exception("Product not found");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.CategoryId = product.CategoryId;

        return existingProduct;
    }

    // Delete a product (without saving changes)
    public bool DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        _context.Products.Remove(product);
        return true;
    }
}
