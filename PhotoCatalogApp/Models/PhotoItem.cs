namespace PhotoCatalogApp.Models
{
    public class PhotoItem
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }  // Nowa właściwość
        public double Weight { get; set; }
        public int EstimatedYear { get; set; }
        public string Description { get; set; }
    }
}
