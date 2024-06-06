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
                FilePath = _filePath,
                Width = width,
                Height = height,
                Depth = depth,
                Weight = weight,
                EstimatedYear = estimatedYear,
                Description = Field16.Text,
                // Dodaj pozostałe pola zgodnie z nowym układem
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

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "1. Nazwa" || textBox.Text == "2. Nr ewidencyjny" || textBox.Text == "3. Autor/wytwórca" ||
                textBox.Text == "4. Pochodzenie" || textBox.Text == "5. Wartość w dniu nabycia (szacowana)" || textBox.Text == "6. Czas powstania" ||
                textBox.Text == "7. Miejsce powstania" || textBox.Text == "8. Materiał" || textBox.Text == "9. Technika wykonania" ||
                textBox.Text == "10. Waga" || textBox.Text == "11. Wymiary (wysokość)" || textBox.Text == "11. Wymiary (szerokość)" ||
                textBox.Text == "11. Wymiary (głębokość)" || textBox.Text == "12. Cechy szczególne" || textBox.Text == "13. Dane przekazującego" ||
                textBox.Text == "14. Miejsce znalezienia" || textBox.Text == "15. Data wpisu" || textBox.Text == "16. Uwagi" ||
                textBox.Text == "17. Stan zachowania" || textBox.Text == "18. Zabiegi konserwacyjne")
            {
                textBox.Text = "";
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "Field1") textBox.Text = "1. Nazwa";
                if (textBox.Name == "Field2") textBox.Text = "2. Nr ewidencyjny";
                if (textBox.Name == "Field3") textBox.Text = "3. Autor/wytwórca";
                if (textBox.Name == "Field4") textBox.Text = "4. Pochodzenie";
                if (textBox.Name == "Field5") textBox.Text = "5. Wartość w dniu nabycia (szacowana)";
                if (textBox.Name == "Field6") textBox.Text = "6. Czas powstania";
                if (textBox.Name == "Field7") textBox.Text = "7. Miejsce powstania";
                if (textBox.Name == "Field8") textBox.Text = "8. Materiał";
                if (textBox.Name == "Field9") textBox.Text = "9. Technika wykonania";
                if (textBox.Name == "Field10") textBox.Text = "10. Waga";
                if (textBox.Name == "Field11a") textBox.Text = "11. Wymiary (wysokość)";
                if (textBox.Name == "Field11b") textBox.Text = "11. Wymiary (szerokość)";
                if (textBox.Name == "Field11c") textBox.Text = "11. Wymiary (głębokość)";
                if (textBox.Name == "Field12") textBox.Text = "12. Cechy szczególne";
                if (textBox.Name == "Field13") textBox.Text = "13. Dane przekazującego";
                if (textBox.Name == "Field14") textBox.Text = "14. Miejsce znalezienia";
                if (textBox.Name == "Field15") textBox.Text = "15. Data wpisu";
                if (textBox.Name == "Field16") textBox.Text = "16. Uwagi";
                if (textBox.Name == "Field17") textBox.Text = "17. Stan zachowania";
                if (textBox.Name == "Field18") textBox.Text = "18. Zabiegi konserwacyjne";
            }
        }
    }
}
