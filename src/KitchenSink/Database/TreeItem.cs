using System.Collections.Generic;
using Starcounter;

namespace KitchenSink.Database
{
    // Database class used by Breadcrumb page.
    [Database]
    public class TreeItem
    {
        public string Name { get; set; }
        public TreeItem Parent { get; set; }

        public IEnumerable<TreeItem> Children =>
            Db.SQL<TreeItem>("SELECT i FROM KitchenSink.Database.TreeItem i WHERE Parent = ?", this);
    }
}
