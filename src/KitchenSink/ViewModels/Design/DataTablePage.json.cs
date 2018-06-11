using System.Collections.Generic;
using KitchenSink.Database;
using Starcounter;

namespace KitchenSink.ViewModels.Design
{
    partial class DataTablePage : Json
    {
        private List<string> ColumnHeaders => new List<string> {"First Name", "Last Name", "Email"};

        public IEnumerable<TableRow> Rows => Db.SQL<TableRow>($"SELECT r FROM {typeof(TableRow)} r");

        public void PopulateColumns()
        {
            foreach (var columnHeader in ColumnHeaders)
            {
                var column = this.Columns.Add();
                column.Header = columnHeader;
            }
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
