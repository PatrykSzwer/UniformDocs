using Starcounter;
using System.Linq;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class PaginationPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            GetNewPage();
            SetTotalPages();
            CreateNavButtons();
            SetPageEntries();
        }

        public long TotalEntries = Db.SQL<long>("SELECT COUNT(e) FROM UniformDocs.Database.Book e").FirstOrDefault();

        void Handle(Input.EntriesPerPage action)
        {
            this.CurrentOffset = 0;
            EntriesPerPage = action.Value;
            SetTotalPages();
            GetNewPage();
        }

        void Handle(Input.CurrentPage action)
        {
            this.CurrentOffset = this.EntriesPerPage * (action.Value - 1);
            GetNewPage();
        }

        private void SetPageEntries()
        {
            int[] entriesPerPageAlternatives = new int[] { 5, 15, 30 };
            foreach (int entry in entriesPerPageAlternatives)
            {
                PageEntriesElementJson page = PageEntries.Add();
                page.Amount = entry;
                page.Text = $"Show {entry} items per page";
            }
        }

        private void SetTotalPages()
        {
            long totalPagesRounded = this.TotalEntries / this.EntriesPerPage;
            this.TotalPages = this.TotalEntries % this.EntriesPerPage == 0 ? totalPagesRounded : totalPagesRounded + 1;
        }

        // Establishes the navigation buttons.
        // Displays all the page buttons if there are less then 10,
        // Otherwise it shows the current page, the two before and after.
        private void CreateNavButtons()
        {
            Pages.Clear();
            this.CurrentPage = this.CurrentOffset / this.EntriesPerPage + 1;

            if (this.TotalPages < 10)
            {
                for (long i = 1; i < this.TotalPages + 1; i++)
                {
                    CreateButton(i);
                }
            }

            else if (TotalPages >= 10)
            {
                long pagesBefore = -2;
                long pagesAfter = 3;

                if (this.CurrentPage + pagesAfter > this.TotalPages)
                {
                    pagesBefore -= (this.CurrentPage + pagesAfter - 1) % this.TotalPages;
                }

                if (this.CurrentPage + pagesBefore <= 0)
                {
                    pagesAfter -= (this.CurrentPage + pagesBefore - 1);
                }

                for (long i = this.CurrentPage + pagesBefore; i < this.CurrentPage + pagesAfter; i++)
                {
                    if (i > 0 && i < this.TotalPages + 1)
                    {
                        CreateButton(i);
                    }
                }
            }

            this.DisableLast = this.CurrentPage == this.TotalPages;
            this.DisableFirst = this.CurrentPage == 1;
        }

        // Creates a nav button by setting the JSON to the current page number and if it's active.
        private void CreateButton(long pageNumber)
        {
            PagesElementJson page = Pages.Add();
            page.PageNumber = pageNumber;
            page.Active = (CurrentPage == pageNumber);
        }

        private void GetNewPage()
        {
            this.Library.Data = Db.SQL<Book>("SELECT b FROM UniformDocs.Database.Book b ORDER BY b.Position FETCH ? OFFSET ?", this.EntriesPerPage, this.CurrentOffset);
            CreateNavButtons();
        }
    }
}