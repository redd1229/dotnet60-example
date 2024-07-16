using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Entities
{
    [PrimaryKey(nameof(Barcode), nameof(PcsId))]
    public class CenterFileRectangle
    {
        public string? Barcode { get; set; }

        public int PcsId { get; set; }

        public double MinX { get; set; }

        public double MaxX { get; set; }

        public double MinY { get; set; }

        public double MaxY { get; set; }
    }
}
