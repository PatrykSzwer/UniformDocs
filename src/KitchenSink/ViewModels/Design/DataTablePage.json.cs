using System.Collections.Generic;
using System.Linq;
using KitchenSink.Database;
using Starcounter;
using Starcounter.Linq;

namespace KitchenSink.ViewModels.Design
{
    partial class DataTablePage : Json
    {
        private List<string> ColumnHeaders => new List<string> {"First Name", "Last Name", "Email"};

        public IEnumerable<TableRow> Rows { get; set; }

        public void Init()
        {
            this.GetFirstRows();
            this.PopulateColumns();
        }

        private void GetFirstRows()
        {
            this.Rows = DbLinq.Objects<TableRow>().Take(100);
        }

        private void PopulateColumns()
        {
            foreach (var columnHeader in ColumnHeaders)
            {
                var column = this.Columns.Add();
                column.Header = columnHeader;
            }
        }

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

            this.GetFirstRows();
        }

        void Handle(Input.Page action)
        {
            this.Rows = Rows.Concat(DbLinq.Objects<TableRow>().Skip((int)action.Value * 100).Take(100));
        }

        [DataTablePage_json.Rows]
        partial class DataTableRow : Json, IBound<TableRow>
        {
            void Handle(Input.DeleteTrigger action)
            {
                this.Data.Delete();
            }
        }
    }
}
