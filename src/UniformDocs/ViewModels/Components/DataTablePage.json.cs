using Starcounter;
using Starcounter.Linq;
using Starcounter.Uniform.Builder;
using Starcounter.Uniform.ViewModels;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTablePage : Json
    {
        static DataTablePage()
        {
            DefaultTemplate.DataTable.InstanceType = typeof(UniDataTable);
        }

        public void Init()
        {
            this.DataTable = new DataTableBuilder<DataTableRow>()
                .WithDataSource(DbLinq.Objects<Person>())
                .WithColumns(columns =>
                    columns
                        .AddColumn(b => b.FirstName, column => column.DisplayName("First Name").Sortable().Filterable())
                        .AddColumn(b => b.LastName, column => column.Sortable().DisplayName("Last Name"))
                        .AddColumn(b => b.Email, column => column.Filterable().Sortable().DisplayName("Email")))
                .Build();
        }

        void Handle(Input.AddNewRowTrigger action)
        {
            Db.Transact(() =>
            {
                new Person
                {
                    FirstName = "New first name",
                    LastName = "New last name",
                    Email = "New email"
                };
            });

            this.DataTable.LoadRows();
        }
    }
}