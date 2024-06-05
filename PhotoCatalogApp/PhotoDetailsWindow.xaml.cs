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
            WidthText.Text = photoItem.Width.ToString();
            HeightText.Text = photoItem.Height.ToString();
            WeightText.Text = photoItem.Weight.ToString();
            EstimatedYearText.Text = photoItem.EstimatedYear.ToString();
            DescriptionText.Text = photoItem.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
