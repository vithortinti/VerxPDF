namespace VerxPDF.Domain.Models
{
    public class PageSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public PageSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public PageSize() { }
    }
}
