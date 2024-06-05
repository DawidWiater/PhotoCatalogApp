using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using PhotoCatalogApp.Data;
using PhotoCatalogApp.Models;
using System.Linq;

namespace PhotoCatalogApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext _dbContext;
        private string _selectedFilePath;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext("Data Source=photoCatalog.db");
            _dbContext.InitializeDatabase();
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;
                FilePathTextBox.Text = _selectedFilePath;
                FileInfo fileInfo = new FileInfo(_selectedFilePath);
                MessageBox.Show($"Rozmiar pliku: {fileInfo.Length / 1024} KB");
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                MessageBox.Show("Proszę wybrać plik przed wysłaniem.");
                return;
            }

            // Przenieś do nowego okna
            var uploadWindow = new UploadWindow(_selectedFilePath);
            uploadWindow.Show();
            this.Close();
        }

        private void ShowAllFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItems = _dbContext.GetAllPhotoItems().ToList();
            // Logika wyświetlania wszystkich plików, np. otwarcie nowego okna z listą plików
            MessageBox.Show($"Wszystkie pliki: {photoItems.Count}");
        }

        private void ShowRecentlyAddedButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItems = _dbContext.GetAllPhotoItems().OrderByDescending(p => p.Id).Take(10).ToList();
            // Logika wyświetlania ostatnio dodanych plików, np. otwarcie nowego okna z listą plików
            MessageBox.Show($"Ostatnio dodane pliki: {photoItems.Count}");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
