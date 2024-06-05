using System;
using System.Windows;
using PhotoCatalogApp.Data;
using PhotoCatalogApp.Models;
using System.Linq;

namespace PhotoCatalogApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext("Data Source=photoCatalog.db");
            _dbContext.InitializeDatabase();
            LoadPhotoItems();
        }

        private void LoadPhotoItems()
        {
            var photoItems = _dbContext.GetAllPhotoItems().ToList();
            PhotoListBox.ItemsSource = photoItems;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItem = new PhotoItem
            {
                FilePath = FilePathTextBox.Text,
                Width = 100.0, // Zaktualizuj rzeczywistymi danymi
                Height = 200.0, // Zaktualizuj rzeczywistymi danymi
                Weight = 300.0, // Zaktualizuj rzeczywistymi danymi
                EstimatedYear = 2023, // Zaktualizuj rzeczywistymi danymi
                Description = "Opis zdjęcia" // Zaktualizuj rzeczywistymi danymi
            };

            _dbContext.AddPhotoItem(photoItem);
            LoadPhotoItems();
        }

        private void ShowAllFilesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPhotoItems();
        }

        private void ShowRecentlyAddedButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItems = _dbContext.GetAllPhotoItems().OrderByDescending(p => p.Id).Take(10).ToList();
            PhotoListBox.ItemsSource = photoItems;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
