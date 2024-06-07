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
            _dbContext.InitializeDatabase();  // Upewnij się, że baza danych jest zainicjalizowana
            SelectedImage.Source = new BitmapImage(new Uri(_filePath));
            FileInfo fileInfo = new FileInfo(_filePath);
            FileSizeTextBlock.Text = $"Rozmiar pliku: {fileInfo.Length / 1024} KB";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            double width, height, depth, weight;
            int estimatedYear;

            if (!double.TryParse(Field11a.Text, out height))
            {
                MessageBox.Show("Proszę wprowadzić prawidłową wysokość.");
                return;
            }

            if (!double.TryParse(Field11b.Text, out width))
            {
                MessageBox.Show("Proszę wprowadzić prawidłową szerokość.");
                return;
            }

            if (!double.TryParse(Field11c.Text, out depth))
            {
                MessageBox.Show("Proszę wprowadzić prawidłową głębokość.");
                return;
            }

            if (!double.TryParse(Field10.Text, out weight))
            {
                MessageBox.Show("Proszę wprowadzić prawidłową wagę.");
                return;
            }

            if (!int.TryParse(Field6.Text, out estimatedYear))
            {
                MessageBox.Show("Proszę wprowadzić prawidłowy szacowany rok.");
                return;
            }

            var photoItem = new PhotoItem
            {
                Name = Field1.Text, // Zapisujemy nazwę pliku
                FilePath = _filePath,
                Width = width,
                Height = height,
                Depth = depth,
                Weight = weight,
                EstimatedYear = estimatedYear,
                Description = Field16.Text,
                Material = Field8.Text // Dodanie materiału
            };

            _dbContext.AddPhotoItem(photoItem);

            // Powrót do MainWindow po dodaniu zdjęcia
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void FillRandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            if (string.IsNullOrWhiteSpace(Field1.Text)) Field1.Text = "Random Name " + rand.Next(1, 100);
            if (string.IsNullOrWhiteSpace(Field2.Text)) Field2.Text = rand.Next(1000, 9999).ToString();
            if (string.IsNullOrWhiteSpace(Field3.Text)) Field3.Text = "Random Author";
            if (string.IsNullOrWhiteSpace(Field4.Text)) Field4.Text = "Random Origin";
            if (string.IsNullOrWhiteSpace(Field5.Text)) Field5.Text = rand.Next(100, 1000).ToString() + " PLN";
            if (string.IsNullOrWhiteSpace(Field6.Text)) Field6.Text = rand.Next(1900, 2022).ToString();
            if (string.IsNullOrWhiteSpace(Field7.Text)) Field7.Text = "Random Place";
            if (string.IsNullOrWhiteSpace(Field8.Text)) Field8.Text = "Random Material";
            if (string.IsNullOrWhiteSpace(Field9.Text)) Field9.Text = "Random Technique";
            if (string.IsNullOrWhiteSpace(Field10.Text)) Field10.Text = rand.NextDouble().ToString("F2");
            if (string.IsNullOrWhiteSpace(Field11a.Text)) Field11a.Text = rand.NextDouble().ToString("F2");
            if (string.IsNullOrWhiteSpace(Field11b.Text)) Field11b.Text = rand.NextDouble().ToString("F2");
            if (string.IsNullOrWhiteSpace(Field11c.Text)) Field11c.Text = rand.NextDouble().ToString("F2");
            if (string.IsNullOrWhiteSpace(Field12.Text)) Field12.Text = "Random Feature";
            if (string.IsNullOrWhiteSpace(Field13.Text)) Field13.Text = "Random Donor";
            if (string.IsNullOrWhiteSpace(Field14.Text)) Field14.Text = "Random Location";
            if (string.IsNullOrWhiteSpace(Field15.Text)) Field15.Text = DateTime.Now.ToShortDateString();
            if (string.IsNullOrWhiteSpace(Field16.Text)) Field16.Text = "Random Notes";
            if (string.IsNullOrWhiteSpace(Field17.Text)) Field17.Text = "Random Condition";
            if (string.IsNullOrWhiteSpace(Field18.Text)) Field18.Text = "Random Conservation";
        }
    }
}
