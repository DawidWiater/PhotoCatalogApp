using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PhotoCatalogApp.Models;

namespace PhotoCatalogApp
{
    public partial class FilesWindow : Window
    {
        public FilesWindow(IEnumerable<PhotoItem> photoItems)
        {
            InitializeComponent();
            foreach (var item in photoItems)
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(item.FilePath)),
                    Width = 200,
                    Height = 200,
                    Margin = new Thickness(10),
                    Tag = item
                };

                image.MouseLeftButtonUp += Image_MouseLeftButtonUp;

                FilesGrid.Children.Add(image);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var image = sender as Image;
            var photoItem = image.Tag as PhotoItem;

            var photoDetailsWindow = new PhotoDetailsWindow(photoItem);
            photoDetailsWindow.ShowDialog();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
