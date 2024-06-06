using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PhotoCatalogApp.Models;

namespace PhotoCatalogApp
{
    public partial class FilesWindow : Window
    {
        private List<PhotoItem> _photoItems;

        public FilesWindow(List<PhotoItem> photoItems)
        {
            InitializeComponent();
            _photoItems = photoItems;
            LoadPhotoItems(_photoItems);
        }

        private void LoadPhotoItems(List<PhotoItem> photoItems)
        {
            PhotoItemsControl.Children.Clear();
            foreach (var photoItem in photoItems)
            {
                var button = new Button
                {
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri(photoItem.FilePath)),
                        Width = 200,
                        Height = 200
                    },
                    Tag = photoItem,
                    Margin = new Thickness(5)
                };
                button.Click += PhotoItem_Click;
                PhotoItemsControl.Children.Add(button);
            }
        }

        private void PhotoItem_Click(object sender, RoutedEventArgs e)
        {
            var photoItem = ((Button)sender).Tag as PhotoItem;
            var detailsWindow = new PhotoDetailsWindow(photoItem);
            detailsWindow.Show();
        }

        /* Zakomentowane sortowanie
        private void SortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortByComboBox.SelectedItem != null)
            {
                var selectedSort = ((ComboBoxItem)SortByComboBox.SelectedItem).Content.ToString();
                IEnumerable<PhotoItem> sortedItems = null;

                switch (selectedSort)
                {
                    case "Data dodania":
                        sortedItems = _photoItems.OrderBy(p => p.Id);
                        break;
                    case "Szerokość":
                        sortedItems = _photoItems.OrderBy(p => p.Width);
                        break;
                    case "Wysokość":
                        sortedItems = _photoItems.OrderBy(p => p.Height);
                        break;
                    case "Waga":
                        sortedItems = _photoItems.OrderBy(p => p.Weight);
                        break;
                    case "Szacowany Rok":
                        sortedItems = _photoItems.OrderBy(p => p.EstimatedYear);
                        break;
                }

                LoadPhotoItems(sortedItems.ToList());
            }
        }
        */

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
