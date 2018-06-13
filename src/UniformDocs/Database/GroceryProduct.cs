using Starcounter;

namespace UniformDocs.Database
{
    // Database class used by Autocomplete page.
    [Database]
    public class GroceryProduct
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}
