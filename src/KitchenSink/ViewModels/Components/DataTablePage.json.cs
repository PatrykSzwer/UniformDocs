using System.Collections.Generic;
using System.Linq;
using KitchenSink.Database;
using Starcounter;
using Starcounter.Linq;

namespace KitchenSink.ViewModels.Components
{
    partial class DataTablePage : Json
    {
        public int RowsCount => DbLinq.Objects<TableRow>().Count();
        public int PageSize => 100;

        public void Init()
        {
            var newRowsData = this.RowsData.Add();
            newRowsData.Rows = DbLinq.Objects<TableRow>().Take(PageSize);
        }

        private void GetPage(int page)
        {
            if (RowsData.ElementAtOrDefault(page) == null)
            {
                var newRowsData = this.RowsData.Add();
                newRowsData.Rows = DbLinq.Objects<TableRow>().Skip(page * PageSize).Take(PageSize);
            }
            else
            {
                this.RowsData.ElementAt(page).Rows = DbLinq.Objects<TableRow>().Skip(page * PageSize).Take(PageSize);
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
        }

        void Handle(Input.Page action)
        {
            this.GetPage((int)action.Value);
        }


        [DataTablePage_json.RowsData]
        partial class DataTableRowsData : Json
        {
            public IEnumerable<TableRow> Rows { get; set; }
        }

        [DataTablePage_json.RowsData.Rows]
        partial class DataTableRow : Json, IBound<TableRow>
        {
            void Handle(Input.DeleteTrigger action)
            {
                this.Data.Delete();
            }
        }
    }
}
