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
                .WithDataSource(DbLinq.Objects<Person>(), data => data.WithConverter(CreateDataTableRow).WithFilter(new PersonFilter()))
                .WithColumns(columns =>
                    columns
                        .AddColumn(b => b.FirstName, column => column.DisplayName("First Name").Sortable().Filterable())
                        .AddColumn(b => b.LastName, column => column.Sortable().DisplayName("Last Name"))
                        .AddColumn(b => b.Email.Address, column => column.Filterable().Sortable().DisplayName("Email")))
                .WithInitialPageSize(20)
                .Build();
        }

        private DataTableRow CreateDataTableRow(Person person)
        {
            var newDataTableRow = new DataTableRow
            {
                DeleteAction = DeleteTableRow,
                Data = person
            };

            return newDataTableRow;
        }

        private void DeleteTableRow(DataTableRow tableRow)
        {
            Db.Transact(() => { tableRow.Data.Delete(); });
            this.DataTable.LoadRows();
        }

        void Handle(Input.AddNewRowTrigger action)
        {
            Db.Transact(() =>
            {
                new Person
                {
                    FirstName = "New first name",
                    LastName = "New last name",
                    Email = new Email
                    {
                        Address = "newPersonsEmail@email.com",
                        Type = "Private"
                    }
                };
            });

            this.DataTable.LoadRows();
        }
    }
}