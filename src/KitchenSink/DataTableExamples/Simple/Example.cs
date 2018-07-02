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
            return new DataTableBuilder<BookViewModel>()
                .WithDataSource(DbLinq.Objects<Book>())
                .WithColumns(columns =>
                    columns
                        .AddColumn(b => b.Position, column => column.DisplayName("no. "))
                        .AddColumn(b => b.Title)
                        .AddColumn(b => b.Author))

                .Build();
        }
    }
}