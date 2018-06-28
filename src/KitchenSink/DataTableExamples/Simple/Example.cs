using KitchenSink.Database;
using Starcounter;
using Starcounter.Linq;
using Starcounter.Uniform.Builder;

namespace KitchenSink.DataTableExamples.Simple
{
    public class Example
    {
        public Json DoExample()
        {
            return new DataTableBuilder<Book, BookViewModel>(DbLinq.Objects<Book>())
                .AddColumn(b => b.Position, isSortable: true, isFilterable: true, displayName: "no.")
                .AddColumn(b => b.Title, isSortable: true, isFilterable: true, displayName: "Title")
                .AddColumn(b => b.Author, isSortable: true, isFilterable: true, displayName: "Author")
                .Build();
        }
    }
}