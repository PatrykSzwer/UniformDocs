using Starcounter;

namespace KitchenSink.Database
{
    // Database class used by Pagination page.
    [Database]
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public long Position { get; set; }
    }
}
