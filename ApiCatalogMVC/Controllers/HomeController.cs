using ApiCatalogMVC.Services.Interfaces;
using ApiCatalogMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiCatalogMVC.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;

    public HomeController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
    {
        var result = await _categoryService.GetAllCategoriesAsync();

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewModel>> CreateCategory(CategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.CreateCategory(category);
            
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.Error = "Erro ao criar uma nova categoria";
        return View(category);
    }

    [HttpGet]
    public async Task<ActionResult<CategoryViewModel>> UpdateCategory(int id)
    {
        var result = await _categoryService.GetCategoryAsync(id);

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewModel>> UpdateCategory(int id, CategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, category);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.Error = "Erro ao atualizar a categoria";
        return View(category);
    }

    [HttpGet]
    public async Task<ActionResult<CategoryViewModel>> DeleteCategory(int id)
    {
        var result = await _categoryService.GetCategoryAsync(id);

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpPost, ActionName("DeleteCategory")]
    public async Task<IActionResult> ConfirmDeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        
        if (result) return RedirectToAction(nameof(Index));

        ViewBag.Error = "Erro ao deletar categoria";
        return View();
    }
}