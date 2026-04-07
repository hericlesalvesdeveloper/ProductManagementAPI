using ProductManagementAPI.Exceptions;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        if (product.Price <= 0) throw new BussinesException("The price of product has to be greater than zero!");
        return await _productRepository.CreateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        if(id <= 0) throw new BadRequestException("The ID must be greater than zero!");

        var deleted = await _productRepository.DeleteAsync(id);

        if (!deleted) throw new NotFoundException("Product not found");

    }

    public async Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _productRepository.GetAllAsync(page, pageSize);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (id <= 0) throw new BadRequestException("The ID must be greater than zero!");

        var productExists = await _productRepository.GetByIdAsync(id)
        ?? throw new NotFoundException("Product Not Found");

        return productExists;
    }

    public async Task UpdateAsync(int id, Product product)
    {
        if (id <= 0) throw new BadRequestException("The ID must be greater than zero!");

        var updated = await _productRepository.UpdateAsync(id, product);

        if (!updated) throw new NotFoundException("Product not found");
    }
}
