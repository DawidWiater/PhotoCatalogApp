using System.Windows;
using PhotoCatalogApp.Data;
using PhotoCatalogApp.Models;

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
            var photoItems = _dbContext.GetAllPhotoItems();
            // Update UI to display photo items (e.g., populate a ListView or ListBox)
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var photoItem = new PhotoItem
            {
                FilePath = "path/to/photo.jpg", // Update with actual file path
                Width = 100.0, // Update with actual width
                Height = 200.0, // Update with actual height
                Weight = 300.0, // Update with actual weight
                EstimatedYear = 2023, // Update with actual estimated year
                Description = "Description of the photo" // Update with actual description
            };

            _dbContext.AddPhotoItem(photoItem);
            LoadPhotoItems();
        }
    }
}
