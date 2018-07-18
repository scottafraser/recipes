using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using RecipeBox.ViewModels;

namespace RecipeBox.Controllers
{
    public class RecipesController : Controller
    {

        [HttpGet("/recipes/index")]
        public ActionResult Index()
        {
            List<Recipe> allRecipes = Recipe.GetAll();

            return View(allRecipes);
        }

        [HttpGet("/recipes/{id}/details")]
        public ActionResult Details(int id)
        {
            RecipeData newRecipe = new RecipeData();
            newRecipe.FindRecipe(id);

            return View(newRecipe);
        }
   
    
    }
}