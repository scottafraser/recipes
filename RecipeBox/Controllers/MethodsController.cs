using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.ViewModels;
using RecipeBox.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeBox.Controllers
{
    public class MethodsController : Controller
    {
        [HttpGet("recipes/{id}/methods/new")]
        public ActionResult MethodForm(int id)
        {
            RecipeData newData = new RecipeData();
            newData.FindRecipe(id);

            return View(newData);
        }

        [HttpPost("/recipes/{id}/methods/new")]
        public ActionResult Create(int id, string step)
        {
            Recipe foundRecipe = Recipe.Find(id);
            Method newMethod = new Method(step);
            newMethod.Save();
            newMethod.AddRecipe(foundRecipe);
            return RedirectToAction("MethodForm", new { id = foundRecipe.Id } );
        }
    }
}
