using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApiCatalogMVC.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private string _token;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProductsAsync(GetJwtToken());

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoriaId = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoriaId", "Nome");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> CreateProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProductAsync(product, GetJwtToken());
            
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.CategoriaId = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoriaId", "Nome");
        return View(product);
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> UpdateProduct(int id)
    {
        var result = await _productService.GetProductAsync(id, GetJwtToken());

        if (result is null) return View("Error");

        ViewBag.CategoriaId = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoriaId", "Nome");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> UpdateProduct(int id, ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProductAsync(id, product, GetJwtToken());

            if (result) return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Erro ao atualizar produto");
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productService.GetProductAsync(id, GetJwtToken());

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> ConfirmDeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id, GetJwtToken());

        if (result) return RedirectToAction(nameof(Index));

        ModelState.AddModelError(string.Empty, "Erro ao deletar produto");
        return View();
    }

    private string GetJwtToken()
    {
        if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
        {
            _token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
        }

        return _token;
    }
}
