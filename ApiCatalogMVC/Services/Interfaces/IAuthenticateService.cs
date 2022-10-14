using ApiCatalogMVC.ViewModels;

namespace ApiCatalogMVC.Services.Interfaces;

public interface IAuthenticateService
{
    Task<TokenViewModel> Register(UserRegisterViewModel user);
    Task<TokenViewModel> Authenticate(UserLoginViewModel user);
}
