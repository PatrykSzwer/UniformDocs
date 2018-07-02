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
                return new DataTableBuilder<BookViewModel>()
                    .WithDataSource(DbLinq.Objects<Book>(), data => data
                        .WithConverter(CreateBookViewModel)
                        .WithFilter(new BookFilter()))
                    .WithColumns(columns =>
                        columns
                            .AddColumn(b => b.Position, column => column
                                .Sortable()
                                .Filterable()
                                .DisplayName("no. "))
                            .AddColumn(b => b.Display, column => column
                                .Sortable()
                                .Filterable()
                                .DisplayName("Author&Title"))
                    )
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