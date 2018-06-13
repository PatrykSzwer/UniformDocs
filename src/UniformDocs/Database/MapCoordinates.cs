using Starcounter;

namespace UniformDocs.Database
{
    // Database class used by Map/Geo page.
    [Database]
    public class MapCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
