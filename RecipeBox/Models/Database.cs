﻿using System;
using MySql.Data.MySqlClient;

namespace RecipeBox.Models
{

    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}