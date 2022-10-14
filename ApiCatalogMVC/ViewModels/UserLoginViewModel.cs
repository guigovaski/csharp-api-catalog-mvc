using System.ComponentModel.DataAnnotations;

namespace ApiCatalogMVC.ViewModels;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "E-mail deve ser informado")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha deve ser informada")]
    public string? Password { get; set; }
}
