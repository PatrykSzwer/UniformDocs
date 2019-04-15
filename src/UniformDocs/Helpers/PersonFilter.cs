using Starcounter.Uniform.Generic.FilterAndSort;
using Starcounter.Uniform.Queryables;
using System;
using System.Linq;
using UniformDocs.Database;

namespace UniformDocs.Helpers
{
    public class PersonFilter : QueryableFilter<Person>
    {
        protected override IQueryable<Person> ApplyFilter(IQueryable<Person> data, Filter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.PropertyName == nameof(Email.Address))
            {
                return data.Where(person => person.Email.Address.Contains(filter.Value));
            }

            return base.ApplyFilter(data, filter);
        }

        protected override IQueryable<Person> ApplyOrder(IQueryable<Person> data, Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (order.PropertyName == nameof(Email.Address))
            {
                return order.Direction == OrderDirection.Ascending ?
                    data.OrderBy(person => person.Email.Address) :
                    data.OrderByDescending(person => person.Email.Address);
            }

            return base.ApplyOrder(data, order);
        }
    }
}
