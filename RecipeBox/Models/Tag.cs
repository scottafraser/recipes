using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Tag(string name, int id = 0)
        {
            Name = name;
            Id = id;
        }

        public override bool Equals(System.Object otherTag)
        {
            if (!(otherTag is Tag))
            {
                return false;
            }
            else
            {
                Tag newTag = (Tag)otherTag;
                bool idEquality = Id == newTag.Id;
                bool nameEquality = Name == newTag.Name;
                return (idEquality && nameEquality);
            }
        }


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tags (name) VALUES (@name);";

            cmd.Parameters.AddWithValue("@name", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
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
            cmd.CommandText = @"DELETE FROM tags;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tags SET name = @newName WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            cmd.Parameters.AddWithValue("@newName", newName);

            cmd.ExecuteNonQuery();
            Name = newName;

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
            cmd.CommandText = @"DELETE FROM tags WHERE id = @tagId; DELETE FROM tags_recipes WHERE tag_id = @tagId";
            cmd.Parameters.AddWithValue("@tagId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Tag> GetAll()
        {
            List<Tag> allTags = new List<Tag> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);

                Tag newTag = new Tag(name, id);
                allTags.Add(newTag);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTags;
        }

        public static Tag Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int tagId = 0;
            string name = "";

            while (rdr.Read())
            {
                tagId = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }

            Tag newTag = new Tag(name, tagId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newTag;
        }


        public void AddRecipe(Recipe newRecipe)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tags_recipes (recipe_id, tag_id) VALUES (@recipeId, @tagId);";

            cmd.Parameters.AddWithValue("@tagId", this.Id);
            cmd.Parameters.AddWithValue("@recipeId", newRecipe.Id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteRecipe(Recipe newRecipe)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tags_recipes WHERE recipe_id = @recipeId AND tag_id = @tagId;";
            cmd.Parameters.AddWithValue("@recipeId", newRecipe.Id);
            cmd.Parameters.AddWithValue("@tagId", this.Id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Recipe> GetRecipes()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT recipes.* FROM tags
            JOIN tags_recipes ON (tags.id = tags_recipes.tag_id)
            JOIN recipes ON (tags_recipes.recipe_id = recipes.id)
            WHERE tags.id = @tagId;";

            cmd.Parameters.AddWithValue("@tagId", this.Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Recipe> recipes = new List<Recipe> { };

            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                int rating = rdr.GetInt32(2);
                string ingredients = rdr.GetString(3);
                string method = rdr.GetString(4);


                Recipe newRecipe = new Recipe(name, rating, ingredients, method, id);
                recipes.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return recipes;
        }
    }
}