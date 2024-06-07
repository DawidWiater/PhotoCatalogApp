namespace PhotoCatalogApp.Models
{
    public class PhotoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } // Dodajemy właściwość Name
        public string FilePath { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public int EstimatedYear { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
    }
}
