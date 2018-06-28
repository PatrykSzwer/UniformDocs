using System.Linq;
using KitchenSink.Database;
using Starcounter.Uniform.Generic.FilterAndSort;
using Starcounter.Uniform.Queryables;

namespace KitchenSink.DataTableExamples.Complex
{
    public class BookSorterFilter : QueryableFilterSorter<Book>
    {
        protected override IQueryable<Book> ApplyFilter(IQueryable<Book> data, Filter filter)
        {
            if (filter.PropertyName == nameof(BookViewModel.Display))
            {
                return data.Where(book => book.Author == filter.Value || book.Title == filter.Value);
            }

            return base.ApplyFilter(data, filter);
        }

    }
}