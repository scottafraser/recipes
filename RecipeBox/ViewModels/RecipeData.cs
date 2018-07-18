using System;
using System.Collections.Generic;
using RecipeBox.Models;

namespace RecipeBox.ViewModels
{
    public class RecipeData
    {
        public List<Recipe> AllRecipes { get; set; }
        public List<Tag> AllTags { get; set; }
        public List<Method> AllMethods { get; set; }
        public Recipe FoundRecipe { get;  set; }
        public Tag FoundTag { get; set; }
        public Method FoundMethod { get; set; }

        public RecipeData()
        {
            AllRecipes = Recipe.GetAll();
            AllMethods = Method.GetAll();
            AllTags = Tag.GetAll();
        }

        public void FindRecipe(int id)
        {
            FoundRecipe = Recipe.Find(id);
        }

        public void FindTag(int id)
        {
            FoundTag = Tag.Find(id);
        }

        public void FindMethod(int id)
        {
            FoundMethod = Method.Find(id);
        }


    }
}
