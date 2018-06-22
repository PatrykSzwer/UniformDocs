using System.Collections.Generic;
using Starcounter;

namespace UniformDocs.Database
{
    // Database class used by Breadcrumb page.
    [Database]
    public class TreeItem
    {
        public string Name { get; set; }
        public TreeItem Parent { get; set; }

        public IEnumerable<TreeItem> Children =>
            Db.SQL<TreeItem>("SELECT i FROM UniformDocs.Database.TreeItem i WHERE Parent = ?", this);
    }
}
