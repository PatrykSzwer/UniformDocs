using System.Collections.Generic;
using System.Linq;
using KitchenSink.Database;
using Starcounter;
using Starcounter.Linq;

namespace KitchenSink.ViewModels.Design
{
    partial class DataTablePage : Json
    {
        public IEnumerable<TableRow> Rows { get; set; }
        public int RowsCount => DbLinq.Objects<TableRow>().Count();
        public int PageSize => 100;

        public void Init()
        {
            this.Rows = DbLinq.Objects<TableRow>().Take(PageSize);
        }

        private void GetPage(int page)
        {
            this.Rows = Rows.Concat(DbLinq.Objects<TableRow>().Skip(page * PageSize).Take(PageSize));
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
                this.Data.Delete();
            }
        }
    }
}
