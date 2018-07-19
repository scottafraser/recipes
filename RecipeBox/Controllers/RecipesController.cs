﻿using System;
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
            RecipeData allRecipes = new RecipeData();

            return View(allRecipes);
        }

        [HttpGet("/recipes/search")]
        public ActionResult Search(string searchBy)
        {
            List<Recipe> allRecipes = new List<Recipe>();
            Recipe foundRecipe = Recipe.FindByIngredient("%" + searchBy + "%");
            allRecipes.Add(foundRecipe);

            return View(allRecipes);
        }

        [HttpGet("/recipes/{id}/details")]
        public ActionResult Details(int id)
        {
            RecipeData newRecipe = new RecipeData();
            newRecipe.FindRecipe(id);

            return View(newRecipe);
        }

        [HttpGet("/recipes/new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("/recipes/index")]
        public ActionResult Create(string name, string ingredients, string tags)
        {
            Recipe newRecipe = new Recipe(name, 0, ingredients);
            newRecipe.Save();
            Tag newTag = new Tag(tags, newRecipe.Id);
            newTag.Save();
            newRecipe.AddTag(newTag);

            return RedirectToAction("MethodForm", "Methods", new { id = newRecipe.Id });
        }

        [HttpPost("/recipes/delete")]
        public ActionResult Delete()
        {
            Recipe.DeleteAll();

            return RedirectToAction("Index");
        }

        [HttpPost("/recipes/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Recipe newRecipe = Recipe.Find(id);
            newRecipe.Delete();

            return RedirectToAction("Index");
        }

        [HttpPost("/recipes/{id}/rate")]
        public ActionResult Delete(int id, int rate)
        {
            Recipe newRecipe = Recipe.Find(id);
            newRecipe.Edit(newRecipe.Name, rate, newRecipe.Ingredients );

            return RedirectToAction("Details", new { id = newRecipe.Id });
        }
    
    }
}