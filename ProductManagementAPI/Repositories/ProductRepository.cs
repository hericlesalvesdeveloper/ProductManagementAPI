using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories;

public class ProductRepository : IRepository
{
    private readonly ProductContext _context;

    public ProductRepository(ProductContext context)
    {
        _context = context;
    }
    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        product.CreatedAt = DateTime.UtcNow;

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null || product.DeletedAt != null)
            return false;

        product.DeletedAt = DateTime.UtcNow;

        return true;
        
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _context
            .Products
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context
            .Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var productExists = await GetByIdAsync(product.Id);

        if (productExists is null) return false;

        productExists.Name = product.Name;
        productExists.Description = product.Description;
        productExists.Price = product.Price;
        productExists.Stock = product.Stock;

        await _context.SaveChangesAsync();

        return true;

    }
}
