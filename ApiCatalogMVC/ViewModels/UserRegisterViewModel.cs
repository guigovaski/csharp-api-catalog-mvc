using System.ComponentModel.DataAnnotations;

namespace ApiCatalogMVC.ViewModels;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "E-mail é requerido")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres")]
    public string? Password { get; set; }

    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }
}
