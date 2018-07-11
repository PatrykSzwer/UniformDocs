using System.Collections.Generic;
using System.Linq;
using UniformDocs.Database;
using Starcounter;
using Starcounter.Linq;
using System;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTablePage : Json
    {
        public void Init()
        {
            this.DataTable.PopulateColumns();
            this.DataTable.ReloadRows();
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
    }

    [DataTablePage_json.DataTable]
    partial class DataTablePageDataTable : Json
    {
        public int TotalRows { get; set; }

        private List<string> ColumnProperties => new List<string> {
            "FirstName",
            "LastName",
            "Email"
        };


        public void PopulateColumns()
        {
            foreach (var propName in ColumnProperties)
            {
                var column = this.Columns.Add();
                column.Sort = null;
                column.Filter = "";
                column.DisplayName = propName;
                column.PropertyName = propName;
            }
        }

        public void GetPage(int page)
        {
            var rows = GetRows();
            this.TotalRows = rows.Count();

            if (this.TotalRows == 0)
            {
                return;
            }

            if (page > 0)
            {
                // Add missing dummy pages to maintain sparse page indicies in Pages
                while (this.Pages.ElementAtOrDefault(page - 1) == null)
                {
                    this.Pages.Add();
                }
            }

            var newPages = new DataTablePages();

            // Apply sorting specified in Columns
            rows = SortRows(rows);

            // Take a slice of sorted rows
            newPages.Rows = rows.Skip(page * Pagination.PageSize).Take(Pagination.PageSize);
            // Append or replace the specified page in Pages with the slice
            if (this.Pages.ElementAtOrDefault(page) == null)
            {
                this.Pages.Insert(page, newPages);
            }
            else
            {
                this.Pages[page] = newPages;
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
         * Updates TotalRows and Pages using the Column.Filter and
         * .SortingDirection states.
         */
        public void ReloadRows()
        {
            // Remove all old pages
            this.Pages.Clear();

            // Deliver the last requested page in Pages
            this.GetPage((int)this.Pagination.CurrentPageIndex);
        }
    }

    [DataTablePage_json.DataTable.Pagination]
    partial class DataTablePagination : Json
    {
        public DataTablePageDataTable ParentPage => this.Parent as DataTablePageDataTable;
        public int PageSize => 100;
        public int PagesCount
        {
            get
            {
                return (int)Math.Ceiling((double)ParentPage.TotalRows / (double)PageSize);
            }
        }

        void Handle(Input.CurrentPageIndex action)
        {
            ParentPage.GetPage((int)action.Value);
        }
    }

    [DataTablePage_json.DataTable.Pages]
    partial class DataTablePages : Json
    {
        public IEnumerable<TableRow> Rows { get; set; }
    }

    [DataTablePage_json.DataTable.Pages.Rows]
    partial class DataTableRow : Json, IBound<TableRow>
    {
        void Handle(Input.DeleteTrigger action)
        {
            this.Data.Delete();
        }
    }

    [DataTablePage_json.DataTable.Columns]
    partial class DataTableColumns : Json
    {
        public DataTablePageDataTable ParentPage => this.Parent.Parent as DataTablePageDataTable;

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