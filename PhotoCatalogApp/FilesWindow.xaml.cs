﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            PhotoItemsListView.ItemsSource = photoItems;
            PhotoItemsListView.SelectionChanged += PhotoItemsListView_SelectionChanged;
        }

        private void PhotoItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhotoItemsListView.SelectedItem is PhotoItem selectedPhotoItem)
            {
                var detailsWindow = new PhotoDetailsWindow(selectedPhotoItem);
                detailsWindow.Show();
            }
        }

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
                    case "Nazwa":
                        sortedItems = _photoItems.OrderBy(p => p.Name);
                        break;
                    case "Materiał":
                        sortedItems = _photoItems.OrderBy(p => p.Material);
                        break;
                }

                LoadPhotoItems(sortedItems.ToList());
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
