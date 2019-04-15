using Starcounter;

namespace UniformDocs.Database
{
    [Database]
    public class Email
    {
        public string Type { get; set; }
        public string Address { get; set; }
    }
}
