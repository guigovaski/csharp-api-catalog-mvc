using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogMVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthenticateService _authenticateService;

    public AuthenticationController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<UserRegisterViewModel>> Register(UserRegisterViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _authenticateService.Register(user);
        
            if (result != null)
            {
                Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return Redirect("/");
            }
        }

        ModelState.AddModelError(string.Empty, "Erro ao registrar usuário");
        return View(user);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<ActionResult<UserLoginViewModel>> Login(UserLoginViewModel user)
    {   
        if (ModelState.IsValid)
        {
            var result = await _authenticateService.Authenticate(user);
            
            if (result != null)
            {
                Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return Redirect("/");
            }
        }

        ModelState.AddModelError(string.Empty, "Login inválido!");

        return View(user);
    }
}
