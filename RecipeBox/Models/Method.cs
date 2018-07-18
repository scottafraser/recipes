using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace RecipeBox.Models

{
    public class Method
    {
        public int Id { get; set; }
        public string Step { get; set; }

        public Method(string step, int id = 0)
        {
            Step = step;
            Id = id;
        }


        public override bool Equals(System.Object otherMethod)
        {
            if (!(otherMethod is Method))
            {
                return false;
            }
            else
            {
                Method newMethod = (Method)otherMethod;
                bool idEquality = Id == newMethod.Id;
                bool StepEquality = Step == newMethod.Step;
                return (idEquality && StepEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO methods (step) VALUES (@Step);";

            cmd.Parameters.AddWithValue("@Step", this.Step);

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
            cmd.CommandText = @"DELETE FROM methods WHERE id = @methodId; DELETE FROM methods_recipes WHERE method_id = @methodId";
            cmd.Parameters.AddWithValue("@methodId", this.Id);
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
            cmd.CommandText = @"DELETE FROM methods;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string step)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE methods SET step = @newStep WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            cmd.Parameters.AddWithValue("@newStep", newStep);

            cmd.ExecuteNonQuery();
            Step = newStep;



            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Method> GetAll()
        {
            List<Method> allMethods = new List<Method> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM methods;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string step = rdr.GetString(1);


                Method newMethod = new Method(step, id);
                allMethods.Add(newMethod);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allMethods;
        }

        public static Method Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM methods WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int recipeId = 0;
            string step = "";

            while (rdr.Read())
            {
                recipeId = rdr.GetInt32(0);
                step = rdr.GetString(1);
            }

            Method newMethod = new Method(step, methodId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newMethod;
        }


        public void AddRecipe(Recipe newRecipe)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO methods_recipes (method_id, recipe_id) VALUES (@methodId, @recipeId);";

            cmd.Parameters.AddWithValue("@methodId", this.Id);
            cmd.Parameters.AddWithValue("@recipeId", newRecipe.Id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}