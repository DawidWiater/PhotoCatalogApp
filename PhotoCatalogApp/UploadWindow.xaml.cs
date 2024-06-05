using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PhotoCatalogApp.Data;
using PhotoCatalogApp.Models;

namespace PhotoCatalogApp
{
    public partial class UploadWindow : Window
    {
        private readonly string _filePath;
        private readonly DatabaseContext _dbContext;

        public UploadWindow(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;
            _dbContext = new DatabaseContext("Data Source=photoCatalog.db");
            SelectedImage.Source = new BitmapImage(new Uri(_filePath));
            FileInfo fileInfo = new FileInfo(_filePath);
            FileSizeTextBlock.Text = $"Rozmiar pliku: {fileInfo.Length / 1024} KB";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            double width, height, weight;
            int estimatedYear;

            if (!double.TryParse(WidthTextBox.Text, out width) || WidthTextBox.Text == "Szerokość (cm)")
            {
                MessageBox.Show("Proszę wprowadzić prawidłową szerokość.");
                return;
            }

            if (!double.TryParse(HeightTextBox.Text, out height) || HeightTextBox.Text == "Wysokość (cm)")
            {
                MessageBox.Show("Proszę wprowadzić prawidłową wysokość.");
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out weight) || WeightTextBox.Text == "Waga (kg)")
            {
                MessageBox.Show("Proszę wprowadzić prawidłową wagę.");
                return;
            }

            if (!int.TryParse(EstimatedYearTextBox.Text, out estimatedYear) || EstimatedYearTextBox.Text == "Szacowany Rok")
            {
                MessageBox.Show("Proszę wprowadzić prawidłowy szacowany rok.");
                return;
            }

            var photoItem = new PhotoItem
            {
                FilePath = _filePath,
                Width = width,
                Height = height,
                Weight = weight,
                EstimatedYear = estimatedYear,
                Description = DescriptionTextBox.Text == "Opis" ? "" : DescriptionTextBox.Text
            };

            _dbContext.AddPhotoItem(photoItem);

            // Powrót do MainWindow po dodaniu zdjęcia
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Szerokość (cm)" || textBox.Text == "Wysokość (cm)" || textBox.Text == "Waga (kg)" || textBox.Text == "Szacowany Rok" || textBox.Text == "Opis")
            {
                textBox.Text = "";
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "WidthTextBox") textBox.Text = "Szerokość (cm)";
                if (textBox.Name == "HeightTextBox") textBox.Text = "Wysokość (cm)";
                if (textBox.Name == "WeightTextBox") textBox.Text = "Waga (kg)";
                if (textBox.Name == "EstimatedYearTextBox") textBox.Text = "Szacowany Rok";
                if (textBox.Name == "DescriptionTextBox") textBox.Text = "Opis";
            }
        }
    }
}
