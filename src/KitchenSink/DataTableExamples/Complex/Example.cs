using KitchenSink.Database;
using Starcounter;
using Starcounter.Linq;
using Starcounter.Uniform.Builder;

namespace KitchenSink.DataTableExamples.Complex
{
    namespace Complex
    {

        public class Example
        {
            public Json DoExample()
            {
                return new DataTableBuilder<Book, BookViewModel>(DbLinq.Objects<Book>())
                    .AddColumn(b => b.Position, isSortable: true, isFilterable: true, displayName: "no.")
                    .AddColumn(b => b.Display, isSortable: true, isFilterable: true, displayName: "Author&Title")
                    .SetConverter(CreateBookViewModel)
                    .SetSorterFilter(new BookSorterFilter())
                    .Build();
            }

            private BookViewModel CreateBookViewModel(Book book)
            {
                var bookViewModel = new BookViewModel()
                {
                    DeleteAction = DeleteBook 
                };
                ((Json) bookViewModel).Data = book;
                return bookViewModel;
            }

            private void DeleteBook(BookViewModel bookViewModel)
            {
                bookViewModel.Data.Delete();
                // somehow notify that data source has changed
            }
        }
    }
}