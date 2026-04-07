using ProductManagementAPI.Models;

namespace ProductManagementAPI.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, Product product);

}   
