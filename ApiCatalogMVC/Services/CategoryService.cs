using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ApiCatalogMVC.Services;

public class CategoryService : ICategoryService
{
    private const string _apiEndpoint = "/api/1/categorias/";

    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IHttpClientFactory _clientFactory;

    private CategoryViewModel _categoryVM;
    private IEnumerable<CategoryViewModel> _categoriesVM;

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };   
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        
        using var response = await client.GetAsync(_apiEndpoint);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _categoriesVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(res, _jsonOptions);

        return _categoriesVM;
    }

    public async Task<CategoryViewModel> GetCategoryAsync(int id)
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        
        using var response = await client.GetAsync(_apiEndpoint + id);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _categoryVM = await JsonSerializer.DeserializeAsync<CategoryViewModel>(res, _jsonOptions);

        return _categoryVM;
    }

    public async Task<CategoryViewModel> CreateCategory(CategoryViewModel category)
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        
        var categoryObj = JsonSerializer.Serialize(category);
        StringContent content = new(categoryObj, Encoding.UTF8, "application/json");
        
        using var response = await client.PostAsync(_apiEndpoint, content);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _categoryVM = await JsonSerializer.DeserializeAsync<CategoryViewModel>(res, _jsonOptions);
        
        return _categoryVM;
    }

    public async Task<bool> UpdateCategoryAsync(int id, CategoryViewModel category)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        using var response = await client.PutAsJsonAsync(_apiEndpoint + id, category);

        if (response.IsSuccessStatusCode) return true;
        else return false;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        using var response = await client.DeleteAsync(_apiEndpoint + id);

        if (response.IsSuccessStatusCode) return true;
        else return false;
    }
}
