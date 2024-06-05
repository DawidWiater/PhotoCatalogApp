using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PhotoCatalogApp.Models;

namespace PhotoCatalogApp
{
    public partial class PhotoDetailsWindow : Window
    {
        public PhotoDetailsWindow(PhotoItem photoItem)
        {
            InitializeComponent();
            PhotoImage.Source = new BitmapImage(new Uri(photoItem.FilePath));
            WidthText.Text = $"{photoItem.Width} cm";
            HeightText.Text = $"{photoItem.Height} cm";
            WeightText.Text = $"{photoItem.Weight} kg";
            EstimatedYearText.Text = photoItem.EstimatedYear.ToString();
            DescriptionText.Text = photoItem.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
