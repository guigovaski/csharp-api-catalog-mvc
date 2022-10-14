using System.ComponentModel.DataAnnotations;

namespace ApiCatalogMVC.ViewModels;

public class CategoryViewModel
{
    public int CategoriaId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Image is required")]
    [Display(Name = "Image")]
    public string? ImagemUrl { get; set; }
}
