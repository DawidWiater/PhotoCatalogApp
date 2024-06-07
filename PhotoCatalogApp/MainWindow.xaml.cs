using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using PhotoCatalogApp.Data;
using System.Windows.Controls;

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

            var uploadWindow = new UploadWindow(_selectedFilePath);
            uploadWindow.Show();
            this.Close();
        }

        private void ShowAllFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItems = _dbContext.GetAllPhotoItems().ToList();
            var filesWindow = new FilesWindow(photoItems);
            filesWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowResetConfirmation(object sender, RoutedEventArgs e)
        {
            ConfirmResetTextBox.Visibility = Visibility.Visible;
            ConfirmResetButton.Visibility = Visibility.Visible;
            ConfirmTextBlock.Visibility = Visibility.Visible;
        }

        private void ResetDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmResetTextBox.Text == "tak-chce-zresetowac-wszytskie-dane")
            {
                _dbContext.ResetDatabase();
                MessageBox.Show("Baza danych została zresetowana.");
                ConfirmResetTextBox.Visibility = Visibility.Collapsed;
                ConfirmResetButton.Visibility = Visibility.Collapsed;
                ConfirmTextBlock.Visibility = Visibility.Collapsed;
                ConfirmResetTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Aby zresetować bazę danych, wpisz poprawny tekst w polu potwierdzenia.");
            }
        }

        private void ShowText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                ConfirmTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
