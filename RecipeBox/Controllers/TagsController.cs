using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class TagsController : Controller
    {

        [HttpGet("/tags/index")]
        public ActionResult Index()
        {
            List<Tag> allTags = Tag.GetAll();

            return View(allTags);
        }

        [HttpGet("/tags/{id}/recipes")]
        public ActionResult TagRecipes(int id)
        {
            Tag newTag = Tag.Find(id);
            List<Recipe> tagRecipes = newTag.GetRecipes();
            return View(tagRecipes);
        }
    }
}
