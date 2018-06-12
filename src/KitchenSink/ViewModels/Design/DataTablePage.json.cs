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
        public int PageSize = 25;
        public int Size;

        public void Init()
        {
            this.PopulateColumns();
            this.GetPage(0);
            this.Size = DbLinq.Objects<TableRow>().Count();
        }

        private void GetPage(int page)
        {
            var offset = page * PageSize;
            var PageRows = DbLinq.Objects<TableRow>().Skip(offset).Take(PageSize);
            if (Rows == null)
            {
                this.Rows = PageRows;
            }
            else
            {
                this.Rows = Rows.Take(offset).Concat(PageRows).Concat(Rows.Skip(offset + PageSize));
            }
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

            this.Size = DbLinq.Objects<TableRow>().Count();

            // TODO: update all pages instead of only the first one?
            this.GetPage(0);
        }

        void Handle(Input.Page action)
        {
            this.GetPage((int)action.Value);
        }

        [DataTablePage_json.Rows]
        partial class DataTableRow : Json, IBound<TableRow>
        {
            void Handle(Input.DeleteTrigger action)
            {
                // TODO: Delete the row from the DB
                this.Data.Delete();
                
                ((DataTablePage)Parent.Parent).Size = DbLinq.Objects<TableRow>().Count();
            }
        }
    }
}
