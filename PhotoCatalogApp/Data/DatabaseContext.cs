using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using PhotoCatalogApp.Models;

namespace PhotoCatalogApp.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS PhotoItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FilePath TEXT,
                        Width REAL,
                        Height REAL,
                        Depth REAL,
                        Weight REAL,
                        EstimatedYear INTEGER,
                        Description TEXT
                    )");
            }
        }

        public IEnumerable<PhotoItem> GetAllPhotoItems()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<PhotoItem>("SELECT * FROM PhotoItems");
            }
        }

        public void AddPhotoItem(PhotoItem photoItem)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute(@"
                    INSERT INTO PhotoItems (FilePath, Width, Height, Depth, Weight, EstimatedYear, Description) 
                    VALUES (@FilePath, @Width, @Height, @Depth, @Weight, @EstimatedYear, @Description)", photoItem);
            }
        }

        // Dodatkowe metody do aktualizacji i usuwania danych mogą być dodane tutaj
    }
}
