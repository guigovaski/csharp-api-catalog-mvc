using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiCatalogMVC.Services;

public class ProductService : IProductService
{
    private const string _apiEndpoint = "/api/1/produtos/";
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _jsonOptions;

    private ProductViewModel _product;
    private IEnumerable<ProductViewModel> _products;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string token)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        PutTokenInHeaderAuthorization(token, client);
        using var response = await client.GetAsync(_apiEndpoint);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(res, _jsonOptions);

        return _products;
    }

    public async Task<ProductViewModel> GetProductAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        PutTokenInHeaderAuthorization(token, client);
        using var response = await client.GetAsync(_apiEndpoint + id);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _product = await JsonSerializer.DeserializeAsync<ProductViewModel>(res, _jsonOptions);

        return _product;
    }

    public async Task<ProductViewModel> CreateProductAsync(ProductViewModel product, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        var productObj = JsonSerializer.Serialize(product);
        StringContent content = new(productObj, Encoding.UTF8, "application/json");

        PutTokenInHeaderAuthorization(token, client);
        using var response = await client.PostAsync(_apiEndpoint, content);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _product = await JsonSerializer.DeserializeAsync<ProductViewModel>(res, _jsonOptions);

        return _product;
    }

    public async Task<bool> UpdateProductAsync(int id, ProductViewModel product, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        PutTokenInHeaderAuthorization(token, client);
        using var response = await client.PutAsJsonAsync(_apiEndpoint + id, product);

        if (response.IsSuccessStatusCode) return true;
        else return false;
    }

    public async Task<bool> DeleteProductAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApi");

        PutTokenInHeaderAuthorization(token, client);
        using var response = await client.DeleteAsync(_apiEndpoint + id);

        if (response.IsSuccessStatusCode) return true;
        else return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
