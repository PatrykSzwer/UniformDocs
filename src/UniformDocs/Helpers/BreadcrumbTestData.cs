using System.Linq;
using UniformDocs.Database;
using Starcounter;

/*
Test data hierarchy:

Products
    Food
        Dairy
            Milk
                Coffee milk 5 ML
                Milk 1 L
        Meat
    Metal
        Screws
            Phillips flat head
*/

namespace UniformDocs.Helpers
{
    static class BreadcrumbTestData
    {
        public static bool Exists()
        {
            return Db.SQL<TreeItem>("SELECT i FROM UniformDocs.Database.TreeItem i FETCH ?", 1).Any();
        }

        public static void DeleteAll()
        {
            Db.SlowSQL("DELETE FROM UniformDocs.Database.TreeItem");
        }

        public static void Create()
        {
            Db.Transact(() =>
            {
                var products = new TreeItem
                {
                    Name = "Products"
                };

                var food = new TreeItem
                {
                    Name = "Food",
                    Parent = products
                };

                var dairy = new TreeItem
                {
                    Name = "Dairy",
                    Parent = food
                };

                var milk = new TreeItem
                {
                    Name = "Milk",
                    Parent = dairy
                };

                var coffeeMilk5Ml = new TreeItem
                {
                    Name = "Coffee milk 5 ML",
                    Parent = milk
                };

                var milk1L = new TreeItem
                {
                    Name = "Milk 1 L",
                    Parent = milk
                };

                var meat = new TreeItem
                {
                    Name = "Meat",
                    Parent = food
                };

                var metal = new TreeItem
                {
                    Name = "Metal",
                    Parent = products
                };

                var screws = new TreeItem
                {
                    Name = "Screws",
                    Parent = metal
                };

                var phillipsFlatHead = new TreeItem
                {
                    Name = "Phillips flat head",
                    Parent = screws
                };
            });
        }
    }
}