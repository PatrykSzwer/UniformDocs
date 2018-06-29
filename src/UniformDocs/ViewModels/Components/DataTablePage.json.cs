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

        private List<string> ColumnProperties => new List<string> {
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
            foreach (var propName in ColumnProperties)
            {
                var column = this.Columns.Add();
                column.Sort = null;
                column.Filter = "";
                column.PropertyName = propName;
            }
        }

        private void GetPage(int page)
        {
            var rows = GetRows();
            this.RowsCount = rows.Count();

            if (this.RowsCount == 0)
            {
                return;
            }

            if (page > 0)
            {
                // Add missing dummy pages to maintain sparse page indicies in RowsData
                while (this.RowsData.ElementAtOrDefault(page - 1) == null)
                {
                    this.RowsData.Add();
                }
            }

            var newRowsData = new DataTableRowsData();

            // Apply sorting specified in Columns
            rows = SortRows(rows);

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
        private IEnumerable<TableRow> GetRows()
        {
            var rows = DbLinq.Objects<TableRow>().AsEnumerable();

            rows = FilterRows(rows);

            return rows;
        }

        private IEnumerable<TableRow> SortRows(IEnumerable<TableRow> rowsToSort)
        {
            var rows = rowsToSort.ToList();
            foreach (var column in this.Columns.Where(x => !string.IsNullOrEmpty(x.Sort)))
            {
                if (column.Sort == "asc")
                {
                    rows = rows.OrderBy(x => x.GetType().GetProperty(column.PropertyName)?.GetValue(x)).ToList();
                }
                else if (column.Sort == "desc")
                {
                    rows = rows.OrderByDescending(x => x.GetType().GetProperty(column.PropertyName)?.GetValue(x)).ToList();
                }
            }

            return rows;
        }

        private IEnumerable<TableRow> FilterRows(IEnumerable<TableRow> rowsToFilter)
        {
            IEnumerable<TableRow> rows = null;
            foreach (var column in this.Columns.Where(x => !string.IsNullOrEmpty(x.Filter)))
            {
                if (rows == null)
                {
                    rows = rowsToFilter.Where(row =>
                        row.GetType().GetProperty(column.PropertyName).GetValue(row).ToString().Contains(column.Filter));
                }
                else
                {
                    rows = rows.Concat(rowsToFilter.Where(row =>
                        row.GetType().GetProperty(column.PropertyName).GetValue(row).ToString().Contains(column.Filter)));
                }
            }

            return rows ?? rowsToFilter; // If there are no filters, return all rows
        }

        /**
         * Updates RowsCount and RowsData using the Column.Filter and
         * .SortingDirection states.
         */
        private void ReloadRows()
        {
            // Remove all old pages
            this.RowsData.Clear();

            // Deliver the last requested page in RowsData
            this.GetPage((int)this.Page);
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
            public DataTablePage ParentPage => this.Parent.Parent as DataTablePage;

            void Handle(Input.Filter action)
            {
                this.Filter = action.Value;
                ParentPage.ReloadRows();
            }

            void Handle(Input.Sort action)
            {
                this.Sort = action.Value;
                ParentPage.ReloadRows();
            }
        }
    }
}