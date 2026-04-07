using ProductManagementAPI.Models;

namespace ProductManagementAPI.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<bool> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
}
