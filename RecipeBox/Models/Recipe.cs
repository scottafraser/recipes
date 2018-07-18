using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace RecipeBox.Models
 
{
        public class Recipe
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Rating { get; set; }
            public string Ingredients { get; set; }
            public string Method { get; set; }

            public Recipe(string name, int rating, string ingredients, string method, int id = 0)
            {
                Name = name;
                Rating = rating;
                Ingredients = ingredients;
                Method = method;
                Id = id;
            }


            public override bool Equals(System.Object otherRecipe)
            {
                if (!(otherRecipe is Recipe))
                {
                    return false;
                }
                else
                {
                    Recipe newRecipe = (Recipe)otherRecipe;
                    bool idEquality = Id == newRecipe.Id;
                    bool NameEquality = Name == newRecipe.Name;
                    bool RatingEquality = Rating == newRecipe.Rating;
                    bool IngredientsEquality = Ingredients == newRecipe.Ingredients;
                    bool MethodEquality = Method == newRecipe.Method;
                    return (idEquality && NameEquality && RatingEquality && IngredientsEquality && MethodEquality);
                }
            }

            public void Save()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO recipes (name, rating, ingredients, method) VALUES (@Name, @Rating, @Ingredients, @Method);";

                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Rating", this.Rating);
                cmd.Parameters.AddWithValue("@Ingredients", this.Ingredients);
                cmd.Parameters.AddWithValue("@Method", this.Method);

                cmd.ExecuteNonQuery();
                Id = (int)cmd.LastInsertedId;
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public void Delete()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"DELETE FROM recipes WHERE id = @recipeId; DELETE FROM recipes_tags WHERE recipe_id = @recipeId";
                cmd.Parameters.AddWithValue("@recipeId", this.Id);
                cmd.ExecuteNonQuery();
                conn.Close();

                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public static void DeleteAll()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"DELETE FROM recipes;";
                cmd.ExecuteNonQuery();
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public void Edit(string newName, int newRating, string newIngredients, string newMethod)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"UPDATE items SET name = @newName, rating = @newRating, ingredients = @newIngredients, method = @newMethod WHERE id = @searchId;";

                MySqlParameter searchId = new MySqlParameter();
                searchId.ParameterName = "@searchId";
                searchId.Value = Id;
                cmd.Parameters.Add(searchId);

                cmd.Parameters.AddWithValue("@newName", newName);
                cmd.Parameters.AddWithValue("@newRating", newRating);
                cmd.Parameters.AddWithValue("@newIngredients", newIngredients);
                cmd.Parameters.AddWithValue("@newMethod", newMethod);

                cmd.ExecuteNonQuery();
                Name = newName;
                Rating = newRating;
                Ingredients = newIngredients;



                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public static List<Recipe> GetAll()
            {
                List<Recipe> allRecipes = new List<Recipe> { };
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM recipes;";
                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                while (rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    string name = rdr.GetString(1);
                    int rating = rdr.GetInt32(2);
                    string ingredients = rdr.GetString(3);
                    string method = rdr.GetString(4);


                    Recipe newRecipe = new Recipe(name, rating, ingredients, method, id);
                    allRecipes.Add(newRecipe);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return allRecipes;
            }

            public static Recipe Find(int id)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM recipes WHERE id = (@searchId);";

                MySqlParameter searchId = new MySqlParameter();
                searchId.ParameterName = "@searchId";
                searchId.Value = id;
                cmd.Parameters.Add(searchId);

                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                int recipeId = 0;
                string name = "";
                int rating = 0;
                string ingredients = "";
                string method = "";

                while (rdr.Read())
                {
                    recipeId = rdr.GetInt32(0);
                    name = rdr.GetString(1);
                    rating = rdr.GetInt32(2);
                    ingredients = rdr.GetString(3);
                    method = rdr.GetString(4);
                }

                Recipe newRecipe = new Recipe(name, rating, ingredients, method, recipeId);
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }

                return newRecipe;
            }


            public void AddTag(Tag newTag)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO recipes_tags (recipe_id, tag_id) VALUES (@recipeId, @tagId);";

                cmd.Parameters.AddWithValue("@recipeId", this.Id);
                cmd.Parameters.AddWithValue("@tagId", newTag.Id);

                cmd.ExecuteNonQuery();
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public void DeleteTag(Tag newTag)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"DELETE FROM recipes_tags WHERE recipe_id = @recipeId AND tag_id = @tagId;";
                cmd.Parameters.AddWithValue("@recipeId", this.Id);
                cmd.Parameters.AddWithValue("@tagId", newTag.Id);

                cmd.ExecuteNonQuery();
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public List<Tag> GetTags()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT tags.* FROM recipes
            JOIN recipes_tags ON (recipes.id = recipes_tags.recipe_id)
            JOIN tags ON (recipes_tags.tag_id = tags.id)
            WHERE recipes.id = @recipeId;";

                cmd.Parameters.AddWithValue("@recipeId", this.Id);

                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                List<Tag> tags = new List<Tag> { };

                while (rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    string name = rdr.GetString(1);


                    Tag newTag = new Tag(name, id);
                    tags.Add(newTag);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return tags;
            }
        }
    }