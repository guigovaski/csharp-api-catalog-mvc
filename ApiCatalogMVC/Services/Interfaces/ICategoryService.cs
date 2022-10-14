using ApiCatalogMVC.ViewModels;

namespace ApiCatalogMVC.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
    Task<CategoryViewModel> GetCategoryAsync(int id);
    Task<CategoryViewModel> CreateCategory(CategoryViewModel category);
    Task<bool> UpdateCategoryAsync(int id, CategoryViewModel category);
    Task<bool> DeleteCategoryAsync(int id);
}
