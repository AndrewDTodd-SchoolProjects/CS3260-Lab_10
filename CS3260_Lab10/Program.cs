/*
 * CS3260 lab 10
 * Use this template code and the product.cs class
 * to write LINQ expressions that will produce the values for:
 * Query0 – Find the values in odds that are divisible (no remainder) by three (3) and write these values to the Console.
 * Query1 – Find the values in evens and the values in odds where the evens values are greater than two (2) times the value of odds values. Write these values to the Console.
 * Query2 – Find values in data where the ID is greater than 100 and less than 500 and the Name contains a ‘y’ or Job ends with ‘e’ or Job contains ‘e’ or Job equals “Student” or Job equals “Programmer”. Write these values to the Console.
 * Query 3 – Find values in productList where the ProductID is greater than 10 and less than 66, order in descending order by UnitsInStock and write the ProductName and UnitsInStock to the Console.
 * Query4 – Write a complex query on the productList that shows you know how to use at least ten (10) different LINQ operations i.e. let, where, orderby, into, from, etc.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS3260_Lab12_KH
{
    class Program
    {
        static void Main(string[] args)
        {
            // The following code was provided by the instructor.
            int[] odds = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
            int[] evens = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            var data = new[]
            {
                new { ID = 100, Name = "Grumpy", Job = "Miner" },
                new { ID = 100, Name = "John", Job = "Auto Salesman" },
                new { ID = 200, Name = "Amber", Job = "Scientist" },
                new { ID = 300, Name = "Luke", Job = "M.D." },
                new { ID = 400, Name = "Isabella", Job = "Professor" },
                new { ID = 500, Name = "Liam", Job = "Student" },
                new { ID = 600, Name = "Ava", Job = "Programmer" },
                new { ID = 220, Name = "Royal", Job = "Pharmacist" },
                new { ID = 150, Name = "Meghan", Job = "Princess" },
                new { ID = 175, Name = "Lucas", Job = "Nurse" }
            };
            // END of code provided by the instructor.


            //Query 0 – Find the values in odds that are divisible(no remainder) by three(3) and write these values to the Console.
            IEnumerable<int> queryZero = 
                from num in odds
                where num % 3 == 0
                select num;

            Console.WriteLine("Odd values that are perfectly divisible by 3");
            foreach (var n in queryZero)
            {
                Console.WriteLine($"{n}");
            }
            Console.WriteLine();

            //Query 1 – Find the values in evens and the values in odds where the evens values are greater than two(2) times the value of odds values. Write these values to the Console.
            var queryOne =
                from evenNumber in evens
                let oddNums = odds.Where(n => evenNumber > (2 * n)).Select(n => n)
                where oddNums.Any()
                select new
                {
                    Num = evenNumber,
                    OddsCollection = oddNums
                };

            Console.WriteLine("Even Numbers who are at least twice the value of some number in the odds collection, and the odd numbers the even number is twice as large as");
            foreach (var obj in queryOne)
            {
                Console.WriteLine($"Even number is {obj.Num}\nOdd Numbers that are less than half the even number: ");
                foreach(var oddNum in obj.OddsCollection)
                {
                    Console.Write($"{oddNum}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Query 2 – Find values in data where the ID is greater than 100 and less than 500 and the Name contains a ‘y’ or Job ends with ‘e’ or Job contains ‘e’ or Job equals “Student” or Job equals “Programmer”. Write these values to the Console.
            // The final 3 clauses are completely redundant/unnecessary because the query is already grabbing every item where Job.Contains("e") but I didn't want to act clever and change the query.
            var queryTwo = 
                from person in data
                where person.ID > 100 && person.ID < 500
                where person.Name.Contains('y') || person.Job.EndsWith("e") || person.Job.Contains('e') || person.Job.Equals("Student") || person.Job.Equals("Programmer")
                select person;

            Console.WriteLine("Persons whos ID is between 100 and 500, and whos name contains a y, or job contains an e, or is Sutdent or Programmer");
            foreach(var person in queryTwo)
            {
                Console.WriteLine($"{person.Name} fits the search criteria\nName: {person.Name}, ID: {person.ID}, Job: {person.Job}");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Query 3 – Find values in productList where the ProductID is greater than 10 and less than 66, order in descending order by UnitsInStock and write the ProductName and UnitsInStock to the Console.
            var queryThree = 
                from product in GetProductList()
                where product.ProductID > 10 && product.ProductID < 66
                orderby product.UnitsInStock descending
                select product;

            Console.WriteLine("Products whos ID is between 10 and 66, ordered in decending order based upon unit count");
            foreach(var product in queryThree)
            {
                Console.WriteLine($"Product name: {product.ProductName}, Product ID: {product.ProductID}, Units in Stock: {product.UnitsInStock}");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Query 4 – Write a complex query on the productList that shows you know how to use at least ten(10) different LINQ operations i.e.let, where, orderby, into, from, etc.
            var queryFour =
                from product in GetProductList()
                where !product.Category.Equals("Seafood")
                group product by product.Category into productGroups
                let catagoriesList = productGroups.ToList()
                from catProduct in catagoriesList
                where catProduct.UnitPrice < 60.0M || catProduct.UnitPrice > 80.0M
                orderby catProduct.UnitPrice ascending
                select catProduct;

            Console.WriteLine($"Products that fit the complex query: ");
            foreach (var prod in queryFour)
            {
                Console.WriteLine($"Product Name: {prod.ProductName}, Product ID: {prod.ProductID}:\nCatagory: {prod.Category}, Unit Price: {prod.UnitPrice}, Units in Stock: {prod.UnitsInStock}");
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.ReadKey();
        }
        // The following code was provided by the instructor.
        public static List<Product> GetProductList()
        {
            List<Product> productList = new List<Product>
           {
                new Product{ ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 39 },
                new Product{ ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.0000M, UnitsInStock = 17 },
                new Product{ ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.0000M, UnitsInStock = 13 },
                new Product{ ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.0000M, UnitsInStock = 53 },
                new Product{ ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.3500M, UnitsInStock = 0 },
                new Product{ ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", UnitPrice = 25.0000M, UnitsInStock = 120 },
                new Product{ ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", UnitPrice = 30.0000M, UnitsInStock = 15 },
                new Product{ ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", UnitPrice = 40.0000M, UnitsInStock = 6 },
                new Product{ ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", UnitPrice = 97.0000M, UnitsInStock = 29 },
                new Product{ ProductID = 10, ProductName = "Ikura", Category = "Seafood", UnitPrice = 31.0000M, UnitsInStock = 31 },
                new Product{ ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", UnitPrice = 21.0000M, UnitsInStock = 22 },
                new Product{ ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", UnitPrice = 38.0000M, UnitsInStock = 86 },
                new Product{ ProductID = 13, ProductName = "Konbu", Category = "Seafood", UnitPrice = 6.0000M, UnitsInStock = 24 },
                new Product{ ProductID = 14, ProductName = "Tofu", Category = "Produce", UnitPrice = 23.2500M, UnitsInStock = 35 },
                new Product{ ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", UnitPrice = 15.5000M, UnitsInStock = 39 },
                new Product{ ProductID = 16, ProductName = "Pavlova", Category = "Confections", UnitPrice = 17.4500M, UnitsInStock = 29 },
                new Product{ ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry", UnitPrice = 39.0000M, UnitsInStock = 0 },
                new Product{ ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood", UnitPrice = 62.5000M, UnitsInStock = 42 },
                new Product{ ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections", UnitPrice = 9.2000M, UnitsInStock = 25 },
                new Product{ ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections", UnitPrice = 81.0000M, UnitsInStock = 40 },
                new Product{ ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections", UnitPrice = 10.0000M, UnitsInStock = 3 },
                new Product{ ProductID = 22, ProductName = "Gustaf's Knäckebröd", Category = "Grains/Cereals", UnitPrice = 21.0000M, UnitsInStock = 104 },
                new Product{ ProductID = 23, ProductName = "Tunnbröd", Category = "Grains/Cereals", UnitPrice = 9.0000M, UnitsInStock = 61 },
                new Product{ ProductID = 24, ProductName = "Guaraná Fantástica", Category = "Beverages", UnitPrice = 4.5000M, UnitsInStock = 20 },
                new Product{ ProductID = 25, ProductName = "NuNuCa Nuß-Nougat-Creme", Category = "Confections", UnitPrice = 14.0000M, UnitsInStock = 76 },
                new Product{ ProductID = 26, ProductName = "Gumbär Gummibärchen", Category = "Confections", UnitPrice = 31.2300M, UnitsInStock = 15 },
                new Product{ ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", UnitPrice = 43.9000M, UnitsInStock = 49 },
                new Product{ ProductID = 28, ProductName = "Rössle Sauerkraut", Category = "Produce", UnitPrice = 45.6000M, UnitsInStock = 26 },
                new Product{ ProductID = 29, ProductName = "Thüringer Rostbratwurst", Category = "Meat/Poultry", UnitPrice = 123.7900M, UnitsInStock = 0 },
                new Product{ ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", UnitPrice = 25.8900M, UnitsInStock = 10 },
                new Product{ ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", UnitPrice = 12.5000M, UnitsInStock = 0 },
                new Product{ ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", UnitPrice = 32.0000M, UnitsInStock = 9 },
                new Product{ ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", UnitPrice = 2.5000M, UnitsInStock = 112 },
                new Product{ ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 111 },
                new Product{ ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 20 },
                new Product{ ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", UnitPrice = 19.0000M, UnitsInStock = 112 },
                new Product{ ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", UnitPrice = 26.0000M, UnitsInStock = 11 },
                new Product{ ProductID = 38, ProductName = "Côte de Blaye", Category = "Beverages", UnitPrice = 263.5000M, UnitsInStock = 17 },
                new Product{ ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 69 },
                new Product{ ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood", UnitPrice = 18.4000M, UnitsInStock = 123 },
                new Product{ ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood", UnitPrice = 9.6500M, UnitsInStock = 85 },
                new Product{ ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals", UnitPrice = 14.0000M, UnitsInStock = 26 },
                new Product{ ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages", UnitPrice = 46.0000M, UnitsInStock = 17 },
                new Product{ ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments", UnitPrice = 19.4500M, UnitsInStock = 27 },
                new Product{ ProductID = 45, ProductName = "Rogede sild", Category = "Seafood", UnitPrice = 9.5000M, UnitsInStock = 5 },
                new Product{ ProductID = 46, ProductName = "Spegesild", Category = "Seafood", UnitPrice = 12.0000M, UnitsInStock = 95 },
                new Product{ ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections", UnitPrice = 9.5000M, UnitsInStock = 36 },
                new Product{ ProductID = 48, ProductName = "Chocolade", Category = "Confections", UnitPrice = 12.7500M, UnitsInStock = 15 },
                new Product{ ProductID = 49, ProductName = "Maxilaku", Category = "Confections", UnitPrice = 20.0000M, UnitsInStock = 10 },
                new Product{ ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections", UnitPrice = 16.2500M, UnitsInStock = 65 },
                new Product{ ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce", UnitPrice = 53.0000M, UnitsInStock = 20 },
                new Product{ ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals", UnitPrice = 7.0000M, UnitsInStock = 38 },
                new Product{ ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry", UnitPrice = 32.8000M, UnitsInStock = 0 },
                new Product{ ProductID = 54, ProductName = "Tourtière", Category = "Meat/Poultry", UnitPrice = 7.4500M, UnitsInStock = 21 },
                new Product{ ProductID = 55, ProductName = "Pâté chinois", Category = "Meat/Poultry", UnitPrice = 24.0000M, UnitsInStock = 115 },
                new Product{ ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", UnitPrice = 38.0000M, UnitsInStock = 21 },
                new Product{ ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", UnitPrice = 19.5000M, UnitsInStock = 36 },
                new Product{ ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", UnitPrice = 13.2500M, UnitsInStock = 62 },
                new Product{ ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", UnitPrice = 55.0000M, UnitsInStock = 79 },
                new Product{ ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", UnitPrice = 34.0000M, UnitsInStock = 19 },
                new Product{ ProductID = 61, ProductName = "Sirop d'érable", Category = "Condiments", UnitPrice = 28.5000M, UnitsInStock = 113 },
                new Product{ ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", UnitPrice = 49.3000M, UnitsInStock = 17 },
                new Product{ ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", UnitPrice = 43.9000M, UnitsInStock = 24 },
                new Product{ ProductID = 64, ProductName = "Wimmers gute Semmelknödel", Category = "Grains/Cereals", UnitPrice = 33.2500M, UnitsInStock = 22 },
                new Product{ ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", UnitPrice = 21.0500M, UnitsInStock = 76 },
                new Product{ ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", UnitPrice = 17.0000M, UnitsInStock = 4 },
                new Product{ ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 52 },
                new Product{ ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", UnitPrice = 12.5000M, UnitsInStock = 6 },
                new Product{ ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", UnitPrice = 36.0000M, UnitsInStock = 26 },
                new Product{ ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", UnitPrice = 15.0000M, UnitsInStock = 15 },
                new Product{ ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", UnitPrice = 21.5000M, UnitsInStock = 26 },
                new Product{ ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", UnitPrice = 34.8000M, UnitsInStock = 14 },
                new Product{ ProductID = 73, ProductName = "Röd Kaviar", Category = "Seafood", UnitPrice = 15.0000M, UnitsInStock = 101 },
                new Product{ ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", UnitPrice = 10.0000M, UnitsInStock = 4 },
                new Product{ ProductID = 75, ProductName = "Rhönbräu Klosterbier", Category = "Beverages", UnitPrice = 7.7500M, UnitsInStock = 125 },
                new Product{ ProductID = 76, ProductName = "Lakkalikööri", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 57 },
                new Product{ ProductID = 77, ProductName = "Original Frankfurter grüne Soße", Category = "Condiments", UnitPrice = 13.0000M, UnitsInStock = 32 }
            };
            return productList;
        }
        // END of code provided by the instructor.
    }
}
