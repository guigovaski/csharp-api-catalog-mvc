using ApiCatalogMVC.ViewModels;

namespace ApiCatalogMVC.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string token);
    Task<ProductViewModel> GetProductAsync(int id, string token);
    Task<ProductViewModel> CreateProductAsync(ProductViewModel product, string token);
    Task<bool> UpdateProductAsync(int id, ProductViewModel product, string token);
    Task<bool> DeleteProductAsync(int id, string token);
}
