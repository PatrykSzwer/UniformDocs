using System.Linq;
using KitchenSink.Database;
using Starcounter.Uniform.Generic.FilterAndSort;
using Starcounter.Uniform.Queryables;

namespace KitchenSink.DataTableExamples.Complex
{
    public class BookFilter : QueryableFilter<Book>
    {
        protected override IQueryable<Book> ApplyFilter(IQueryable<Book> data, Filter filter)
        {
            if (filter.PropertyName == nameof(BookViewModel.Display))
            {
                return data.Where(book => book.Author == filter.Value || book.Title == filter.Value);
            }

            return base.ApplyFilter(data, filter);
        }

        protected override IQueryable<Book> ApplyOrder(IQueryable<Book> data, Order order)
        {
            if (order.PropertyName == nameof(BookViewModel.Display) && order.Direction == OrderDirection.Ascending)
            {
                return data.OrderBy(book => book.Author).ThenBy(book => book.Title);
            }
            if (order.PropertyName == nameof(BookViewModel.Display) && order.Direction == OrderDirection.Descending)
            {
                return data.OrderByDescending(book => book.Author).ThenByDescending(book => book.Title);
            }

            return base.ApplyOrder(data, order);
        }
    }
}