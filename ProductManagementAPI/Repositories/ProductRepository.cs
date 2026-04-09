using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories;

public class ProductRepository : IProductRepository
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

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null || product.DeletedAt != null)
            return false;

        product.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
        
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _context
            .Products
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context
            .Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context
            .Products
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        var productExists = await GetByIdAsync(id);

        if (productExists is null) return false;

       _context.Entry(productExists).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return true;
    }
}
