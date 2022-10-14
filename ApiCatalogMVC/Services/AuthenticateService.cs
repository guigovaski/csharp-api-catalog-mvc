using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using System.Text.Json;

namespace ApiCatalogMVC.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string _apiAuthenticateEndpoint = "/api/autoriza/login/";
    private const string _apiRegisterEndpoint = "/api/autoriza/register/";

    private readonly JsonSerializerOptions _jsonOptions;
    private TokenViewModel _token;

    public AuthenticateService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TokenViewModel> Register(UserRegisterViewModel user)
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        using var response = await client.PostAsJsonAsync(_apiRegisterEndpoint, user);

        if (!response.IsSuccessStatusCode) return null;

        var res = await response.Content.ReadAsStreamAsync();
        _token = JsonSerializer.Deserialize<TokenViewModel>(res, _jsonOptions);

        return _token;
    }

    public async Task<TokenViewModel> Authenticate(UserLoginViewModel user)
    {
        var client = _clientFactory.CreateClient("CatalogApi");
        using var response = await client.PostAsJsonAsync(_apiAuthenticateEndpoint, user);

        if (!response.IsSuccessStatusCode) return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();
        _token = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse, _jsonOptions);

        return _token;
    }
}
