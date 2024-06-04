using System.Data.SQLite;
using Dapper;
using System.Collections.Generic;
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
                    INSERT INTO PhotoItems (FilePath, Width, Height, Weight, EstimatedYear, Description) 
                    VALUES (@FilePath, @Width, @Height, @Weight, @EstimatedYear, @Description)", photoItem);
            }
        }

        // Additional methods for update and delete operations can be added here
    }
}
