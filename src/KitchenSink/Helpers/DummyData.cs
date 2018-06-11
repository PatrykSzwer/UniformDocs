using System.Linq;
using KitchenSink.Database;
using Starcounter;

namespace KitchenSink.Helpers
{
    public static class DummyData
    {
        public static void Create()
        {
            Db.Transact(() =>
            {
                // For Dropdown page
                if (!Db.SQL("SELECT p FROM KitchenSink.Database.SoftwareProduct p").Any())
                {
                    new SoftwareProduct { Name = "Starcounter Database" };
                    new SoftwareProduct { Name = "Polymer JavaScript library" };
                }

                // For Pagination page
                if (!Db.SQL<Book>("SELECT b FROM KitchenSink.Database.Book b").Any())
                {
                    // change the number of element with adjusting elementsInTotal
                    int elementsInTotal = 100;
                    for (int i = 0; i < elementsInTotal; i++)
                    {
                        new Book
                        {
                            Author = "Arbitrary Author",
                            Title = "Arbitrary Book " + (i + 1),
                            Position = i + 1
                        };
                    }
                }

                // For Breadcrumb page
                if (BreadcrumbTestData.Exists())
                {
                    BreadcrumbTestData.DeleteAll();
                }
                BreadcrumbTestData.Create();

                // For Autocomplete page
                if (!Db.SQL("SELECT p FROM KitchenSink.Database.GroceryProduct p").Any())
                {
                    new GroceryProduct { Name = "Bread", Price = 1 };
                    new GroceryProduct { Name = "Butter", Price = 3 };
                    new GroceryProduct { Name = "Scotch Whisky", Price = 4 };
                    new GroceryProduct { Name = "Irish Whiskey", Price = 2 };
                    new GroceryProduct { Name = "Milk", Price = 5 };
                    new GroceryProduct { Name = "Boiled Mutton", Price = 7 };
                }

                // For DataTable page
                if (!Db.SQL("SELECT r FROM KitchenSink.Database.TableRow r").Any())
                {
                    for (int i = 0; i < 500; i++)
                    {
                        new TableRow
                        {
                            FirstName = $"Member {i} first name",
                            LastName = $"Member {i} last name",
                            Email = $"Member {i} email"
                        };
                    }
                }
            });
        }
    }
}