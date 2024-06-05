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
                    Margin = new Thickness(10)
                };

                var toolTip = new ToolTip();
                var stackPanel = new StackPanel();
                stackPanel.Children.Add(new TextBlock { Text = "Wymiary:", FontWeight = FontWeights.Bold });
                stackPanel.Children.Add(new TextBlock { Text = $"Szerokość: {item.Width}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Wysokość: {item.Height}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Waga: {item.Weight}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Szacowany rok: {item.EstimatedYear}" });
                stackPanel.Children.Add(new TextBlock { Text = "Opis:", FontWeight = FontWeights.Bold });
                stackPanel.Children.Add(new TextBlock { Text = item.Description });

                toolTip.Content = stackPanel;
                image.ToolTip = toolTip;

                FilesGrid.Children.Add(image);
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
