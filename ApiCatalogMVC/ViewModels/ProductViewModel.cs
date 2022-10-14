using System.ComponentModel.DataAnnotations;

namespace ApiCatalogMVC.ViewModels;

public class ProductViewModel
{
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Descrição é um campo obrigatório")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Preço é um campo obrigatório")]
    public double Price { get; set; }

    [Display(Name = "Imagem")]
    public string? ImagemUrl { get; set; }

    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }
}
