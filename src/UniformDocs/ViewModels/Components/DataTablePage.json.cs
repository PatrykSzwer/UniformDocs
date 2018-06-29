using System.Collections.Generic;
using System.Linq;
using UniformDocs.Database;
using Starcounter;
using Starcounter.Linq;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTablePage : Json
    {
        public int RowsCount { get; set; }
        public int PageSize => 100;

        private List<string> ColumnNames => new List<string> {
            "FirstName",
            "LastName",
            "Email"
        };

        public void Init()
        {
            this.PopulateColumns();
            this.ReloadRows();
        }

        private void PopulateColumns()
        {
            foreach (var columnName in ColumnNames)
            {
                var column = this.Columns.Add();
                column.SortingDirection = null;
                column.Filter = "";
            }
        }

        private void GetPage(int page)
        {
            if (page > 0)
            {
                // Add missing dummy pages to maintain sparse page indicies in RowsData
                while (this.RowsData.ElementAtOrDefault(page - 1) == null)
                {
                    this.RowsData.Add();
                }
            }

            var newRowsData = new DataTableRowsData();

            IQueryable<TableRow> rows = GetRows();

            // Apply sorting specified in Columns
            if (this.Columns[0].SortingDirection == "asc")
            {
                rows = rows.OrderBy(r => r.FirstName);
            }
            else if (this.Columns[0].SortingDirection == "desc")
            {
                rows = rows.OrderByDescending(r => r.FirstName);
            }
            if (this.Columns[1].SortingDirection == "asc")
            {
                rows = rows.OrderBy(r => r.LastName);
            }
            else if (this.Columns[1].SortingDirection == "desc")
            {
                rows = rows.OrderByDescending(r => r.LastName);
            }

            // Take a slice of sorted rows
            newRowsData.Rows = rows.Skip(page * PageSize).Take(PageSize);
            // Append or replace the specified page in RowsData with the slice
            if (this.RowsData.ElementAtOrDefault(page) == null)
            {
                this.RowsData.Insert(page, newRowsData);
            }
            else
            {
                this.RowsData[page] = newRowsData;
            }
        }

        /**
         * Returns rows filtered using Column.Filter states.
         */
        private IQueryable<TableRow> GetRows()
        {
            IQueryable<TableRow> rows = DbLinq.Objects<TableRow>();
            if (this.Columns[2].Filter != "")
            {
                rows = rows.Where(r => r.Email.Contains(this.Columns[2].Filter));
            }
            return rows;
        }

        /**
         * Updates RowsCount and RowsData using the Column.Filter and
         * .SortingDirection states.
         */
        private void ReloadRows()
        {
            // Remove all old pages
            this.RowsData.Clear();

            // Filtering affects the count - update RowsCount
            this.RowsCount = GetRows().Count();

            // If there are any rows to deliver:
            if (this.RowsCount > 0)
            {
                // Deliver the last requested page in RowsData
                this.GetPage((int)this.Page);
            }
            // Else: zero rows, RowsData stays empty
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

        [DataTablePage_json.Columns]
        partial class DataTableColumns : Json
        {
            void Handle(Input.Filter filter)
            {
                // Skip if there was no change
                if (filter.Value == filter.OldValue) return;

                // Apply the change, so that we can read the new value back
                // from Columns while doing filtering in GetRows
                this.Filter = filter.Value;

                // Replace all the RowsData
                ((DataTablePage)this.Parent.Parent).ReloadRows();
            }

            void Handle(Input.SortingDirection sortingDirection)
            {
                // Skip if there was no change
                if (sortingDirection.Value == sortingDirection.OldValue) return;

                // Apply the change, so that we can read the new value back
                // from Columns while doing sorting in GetPage
                this.SortingDirection = sortingDirection.Value;

                // Replace all the RowsData
                ((DataTablePage)this.Parent.Parent).ReloadRows();
            }
        }
    }
}