using Starcounter;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTablePage : Json
    {
        void Handle(Input.AddNewRowTrigger action)
        {
            Db.Transact(() =>
            {
                new TableRow
                {
                    FirstName = "New first name",
                    LastName = "New last name",
                    Email = "New email"
                };
            });
        }
    }
}