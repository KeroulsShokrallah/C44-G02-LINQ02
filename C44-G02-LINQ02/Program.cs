using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace C44_G02_LINQ02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Element Operators

            #region 1. Get first Product out of Stock 
            Console.WriteLine();
            Console.WriteLine("1. Get first Product out of Stock ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            var out_of_stock = ListGenerators.ProductList.FirstOrDefault(x => x.UnitsInStock == 0);
            Console.WriteLine(out_of_stock);
            Console.WriteLine();
            #endregion

            #region 2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.
            Console.WriteLine();
            Console.WriteLine("2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            var fProduct = ListGenerators.ProductList.FirstOrDefault(x => x.UnitPrice > 1000);
            if (fProduct != null)
            {
                Console.WriteLine(fProduct);
            }
            else
            {
                Console.WriteLine("product not found ");
            }
            #endregion


            #region 3. Retrieve the second number greater than 5 
            Console.WriteLine();
            Console.WriteLine("3. Retrieve the second number greater than 5 ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var second = Arr.Where(x => x > 5)
                .Skip(1).FirstOrDefault();
            Console.WriteLine($"second number greater than 5 => {second}");

            #endregion

            #endregion


            #region LINQ - Aggregate Operators

            #region 1. Uses Count to get the number of odd numbers in the array
            Console.WriteLine();
            Console.WriteLine("1. Uses Count to get the number of odd numbers in the array ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int oddcount = Arr2.Count(x => x % 2 != 0);
            Console.WriteLine($"count of odd numbers => {oddcount}");



            #endregion

            #region 2. Return a list of customers and how many orders each has.
            Console.WriteLine();
            Console.WriteLine("2. Return a list of customers and how many orders each has.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var result6 = ListGenerators.CustomerList.Select(c => new
            {
                c.CustomerID,
                c.CustomerName,
                c.Country,
                OrderCount = c.Orders.Count()
            });
            foreach (var item in result6)
            {
                Console.WriteLine(item);
            }


            #endregion

            #region 3. Return a list of categories and how many products each has
            Console.WriteLine();
            Console.WriteLine("3. Return a list of categories and how many products each has");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var resCategory = ListGenerators.ProductList.Select(c => new
            {
                c.Category,
                number_of_products = c.ProductName.Count()
            });

            foreach (var item in resCategory)
            {
                Console.WriteLine(item);

            }
            #endregion

            #region 4. Get the total of the numbers in an array.
            Console.WriteLine();
            Console.WriteLine("Get the total of the numbers in an array.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int[] Arr5 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int totalnumber = Arr5.Sum();

            Console.WriteLine(totalnumber);
            #endregion

            #region  5. Get the total number of characters of all words in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            Console.WriteLine();
            Console.WriteLine("5. Get the total number of characters of all words in dictionary_english.txt (Read dictionary_english.txt into Array of String First).");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            string[] words = File.ReadAllLines("dictionary_english.txt");
            int totalchar = words.Sum(x => x.Length);
            Console.WriteLine(totalchar);
            #endregion

            #region 6. Get the length of the shortest word in dictionary_english.txt
            Console.WriteLine();
            Console.WriteLine("6. Get the length of the shortest word in dictionary_english.txt");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int shortlength = words.Min(x => x.Length);
            Console.WriteLine($" the shortest word in dictionary =>  {shortlength}");

            #endregion

            #region 7. Get the length of the longest word in dictionary
            Console.WriteLine();
            Console.WriteLine("7. Get the length of the longest word in dictionary");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int maxlength = words.Max(x => x.Length);
            Console.WriteLine($"the longest word in dictionary => {maxlength}");
            #endregion

            #region 8. Get the average length of the words in dictionary
            Console.WriteLine();
            Console.WriteLine("Get the average length of the words in dictionary");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();



            int avglength = (int)words.Average(x => x.Length);


            Console.WriteLine($"Get the average length of the words in dictionary => {avglength}");

            #endregion

            #region 9. Get the total units in stock for each product category.
            Console.WriteLine();
            Console.WriteLine("9. Get the total units in stock for each product category.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var totalunits = ListGenerators.ProductList.Where(x => x.UnitsInStock > 0).Select(c => new
            {
                c.ProductID,
                c.ProductName,
                c.UnitsInStock,

            }

            );
            foreach (var item in totalunits)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region 10. Get the cheapest price among each category's products
            Console.WriteLine();
            Console.WriteLine("10. Get the cheapest price among each category's products");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var cheapestPriceForCategory = ListGenerators.ProductList.GroupBy(c => c.Category)
                .Select(g => new
                {
                    category = g.Key,
                    cheapestPriceForCategory = g.Min(x => x.UnitPrice)
                });
            foreach (var item in cheapestPriceForCategory)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.Clear();

            #region 11. Get the products with the cheapest price in each category (Use Let)
            Console.WriteLine();
            Console.WriteLine("11. Get the products with the cheapest price in each category (Use Let)");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            var res = from p in ListGenerators.ProductList
                      group p by p.Category into g
                      let minprice = g.Min(x => x.UnitPrice)
                      from pro in g
                      where pro.UnitPrice == minprice
                      select new
                      {
                          category = g.Key,
                          productname = pro.ProductName,
                          price = pro.UnitPrice
                      };
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region 12. Get the most expensive price among each category's products.
            Console.WriteLine();
            Console.WriteLine("12. Get the most expensive price among each category's products.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var expensivepro = ListGenerators.ProductList.GroupBy(c => c.Category).Select(g => new
            {
                category = g.Key,
                price = g.Max(x => x.UnitPrice)
            });
            foreach (var item in expensivepro)
            { Console.WriteLine(item); }



            #endregion

            #region 13. Get the products with the most expensive price in each category.
            Console.WriteLine();
            Console.WriteLine("13. Get the products with the most expensive price in each category.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var expensive = from p in ListGenerators.ProductList
                            group p by p.Category into g
                            let maxprice = g.Max(x => x.UnitPrice)
                            from pro in g
                            where pro.UnitPrice == maxprice
                            select new
                            {
                                category = g.Key,
                                productname = pro.ProductName,
                                price = pro.UnitPrice
                            };

            foreach (var item in expensive)
            { Console.WriteLine(item); }

            #endregion

            #region 14. Get the average price of each category's products.
            Console.WriteLine();
            Console.WriteLine("14. Get the average price of each category's products.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();


            var avgPerCategory = ListGenerators.ProductList.GroupBy(c => c.Category).Select(g => new
            {
                category = g.Key,
                price = g.Average(x => x.UnitPrice)
            });

            foreach (var item in avgPerCategory)
            { Console.WriteLine(item); }

            #endregion

            #endregion



            #region LINQ - Set Operators

            #region 1. Find the unique Category names from Product List
            Console.WriteLine();
            Console.WriteLine("1. Find the unique Category names from Product List");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var uniqecategory = ListGenerators.ProductList.Select(p => p.Category).Distinct();
            foreach (var item in uniqecategory)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region 2. Produce a Sequence containing the unique first letter from both product and customer names.
            Console.WriteLine();
            Console.WriteLine("2. Produce a Sequence containing the unique first letter from both product and customer names.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var res8 = ListGenerators.ProductList.Select(p => p.ProductName[0]).Union(ListGenerators.CustomerList.Select(c => c.CustomerName[0])).Distinct();

            foreach (var item in res8)

            {
                Console.WriteLine(item);
            }


            #endregion


            #region 3. Create one sequence that contains the common first letter from both product and customer names.
            Console.WriteLine();
            Console.WriteLine("3. Create one sequence that contains the common first letter from both product and customer names.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var res9 = ListGenerators.ProductList.Select(p => p.ProductName[0]).Intersect(ListGenerators.CustomerList.Select(c => c.CustomerName[0])).Distinct();

            foreach (var item in res9)
            { Console.WriteLine(item); }
            #endregion

            #region 4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.
            Console.WriteLine();
            Console.WriteLine("4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var res10 = ListGenerators.ProductList.Select(p => p.ProductName[0]).Except(ListGenerators.CustomerList.Select(c => c.CustomerName[0]));

            foreach (var item in res10)
            { Console.WriteLine(item); }
            #endregion

            #region 5. Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates

            Console.WriteLine();
            Console.WriteLine("5. Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var res11 = ListGenerators.ProductList
                .Select(p => p.ProductName.Substring(p.ProductName.Length - 3))
                .Concat(ListGenerators.CustomerList.Select(c => c.CustomerName.Substring(c.CustomerName.Length - 3)));

            foreach (var item in res11)
            {
                Console.Write(item + " ");

            }
            #endregion

            #endregion


            #region LINQ - Partitioning Operators


            #region 1. Get the first 3 orders from customers in Washington
            Console.WriteLine();
            Console.WriteLine("1. Get the first 3 orders from customers in Washington");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var CustomerinWashington = ListGenerators.CustomerList.Where(c => c.Region == "WA").SelectMany(o => o.Orders).Take(3).ToList();
            if (CustomerinWashington.Any())
            {

                foreach (var item in CustomerinWashington)
                {
                    Console.WriteLine(item);

                }
            }
            else
            {
                Console.WriteLine("no data to show ");
            }
            #endregion


            #region 2. Get all but the first 2 orders from customers in Washington.
            Console.WriteLine();
            Console.WriteLine("2. Get all but the first 2 orders from customers in Washington.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();

            var allcustomer = ListGenerators.CustomerList.Where(c => c.Region == "WA").SelectMany(o => o.Orders.Skip(2), (c, o) => new
            {
                Customername = c.CustomerName,
                city = c.City,
                customerid = c.CustomerID,
                order = o.OrderID,
                orderdate = o.OrderDate
            });
            if (allcustomer.Any())
            {

                foreach (var item in allcustomer)
                {
                    Console.WriteLine(item);

                }
            }
            else
            {
                Console.WriteLine("no data to show ");
            }



            #endregion


            #region 3.Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
            Console.WriteLine();
            Console.WriteLine("3.Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var e = numbers.TakeWhile((num,index)=> num >= index).ToList();
            foreach (var item in e)

            {
                Console.WriteLine(item);
            }




            #endregion


            #region 4.Get the elements of the array starting from the first element divisible by 3.
            Console.WriteLine();
            Console.WriteLine("4.Get the elements of the array starting from the first element divisible by 3.");
            Console.WriteLine("------------------------------------------");

            var elementDivideBy3 = numbers.SkipWhile(x =>  x % 3 != 0);
            foreach (var item in elementDivideBy3)

            {
                Console.Write(item + " ");
            }
            #endregion


            #region 5. Get the elements of the array starting from the first element less than its position.
            Console.WriteLine();
            Console.WriteLine("5. Get the elements of the array starting from the first element less than its position.");
            Console.WriteLine("------------------------------------------");


            var elememtLessThanPosition = numbers.SkipWhile((num, index) => num >= index).ToList();
            foreach (var item in elememtLessThanPosition)

            {
                Console.WriteLine(item);
            }



            #endregion



            #endregion



            #region LINQ - Quantifiers

            #region 1. Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.
            Console.WriteLine();
            Console.WriteLine("1. Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.");
            Console.WriteLine("------------------------------------------");

            var d = words.Any(x => x.Contains("ei"));

            if (d== true)
            {
                Console.WriteLine(" the words in dictionary_english.txt contain the substring 'ei'.");
            }
            else
            {
                Console.WriteLine("no words containes 'ei'");
            }
            #endregion

            #region 2. Return a grouped a list of products only for categories that have at least one product that is out of stock.
            Console.WriteLine();
            Console.WriteLine("2. Return a grouped a list of products only for categories that have at least one product that is out of stock.");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");

            var group = ListGenerators.ProductList.GroupBy(e=> e.Category).Where(x => x.Any(p=> p.UnitsInStock == 0) ).Select(g=> new
            {
                category = g.Key,

      
            });


            foreach (var item in group)
            {
                Console.WriteLine(item);
             

            }

            #endregion


            #region 3. Return a grouped a list of products only for categories that have all of their products in stock.
            Console.WriteLine();
            Console.WriteLine("3. Return a grouped a list of products only for categories that have all of their products in stock.");
            Console.WriteLine("------------------------------------------");

            var groupallinstock = ListGenerators.ProductList.GroupBy(e => e.Category).Where(x => x.All(p => p.UnitsInStock != 0)).Select(g => new
            {
                category = g.Key,
                product = g.ToList()

            });


            foreach(var item in groupallinstock)
            {
                Console.WriteLine($"category => {item.category}" );

                foreach (var p in item.product)
                {
                    Console.WriteLine($"product name => {p.ProductName} - stock => {p.UnitsInStock}");
                }
            }



            #endregion


            #endregion



            #region LINQ – Grouping Operators


            #region 1.Use group by to partition a list of numbers by their remainder when divided by 5
            Console.WriteLine();
            Console.WriteLine("1.Use group by to partition a list of numbers by their remainder when divided by 5");
            Console.WriteLine("------------------------------------------");
            List<int> numbersby5 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groupby5 = numbersby5.GroupBy(x => x % 5).Select(g=> new
            {
                reminder = g.Key,
                numbers = g.ToList()
            });

            foreach(var item in groupby5)
            {
                Console.WriteLine($"number of a remainder of {item.reminder} when divided by 5" );
                foreach(var item2 in item.numbers)
                {
                    Console.WriteLine(item2);
                }
            }
            #endregion


            #region 2.Uses group by to partition a list of words by their first letter. Use dictionary_english.txt for Input
            Console.WriteLine();
            Console.WriteLine("2.Uses group by to partition a list of words by their first letter.\r\n           Use dictionary_english.txt for Input");
            Console.WriteLine("------------------------------------------");


            var firstleteer = words.GroupBy(x => x.First()).Select(g => new
            {
                w = g.Key,
                words = g.ToList()
            });

            //foreach (var item in firstleteer)

            //{
            //    Console.WriteLine($"first letter => {item.w}");
            //    foreach (var w in item.words)
            //    {
            //        Console.WriteLine(w);
            //    }

            //}
            #endregion



            #region 3.Consider this Array as an Input 
            Console.WriteLine();
            Console.WriteLine(" 3.Consider this Array as an Input ");
            Console.WriteLine("------------------------------------------");

                string[] Arr3 = { "from", "salt", "earn", " last", "near", "form" };

            var resarr = Arr3.GroupBy(w => string.Concat(w.OrderBy(c => c))).Select(g => new
            {
                k = g.Key,
                words = g.ToList()
            });

            foreach (var item in resarr)
            {
                Console.WriteLine($"Group => {item.k}");
                foreach (var w in item.words)
                {
                    Console.WriteLine(w);
                }   

            }
            #endregion


            #endregion

            #region LINQ – Transformation Operators

            #region 1. Return a sequence of just the names of a list of products.

            Console.WriteLine();
            Console.WriteLine("1. Return a sequence of just the names of a list of products.");
            Console.WriteLine("------------------------------------------");


            var productname = ListGenerators.ProductList.Select(p => p.ProductName);
            foreach (var name in productname)
            {
                Console.WriteLine(name);
            }

            #endregion

            #region 2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
            Console.WriteLine();
            Console.WriteLine("2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).");
            Console.WriteLine("------------------------------------------");

            string[] wordsarray = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var seq = wordsarray.Select(w => new
            {
                upper = w.ToUpper(),
                lower = w.ToLower()
            });

            foreach (var word in seq)
            {
      
                Console.WriteLine($"word upper => {word.upper} -- word lower => {word.lower}");
            }









            #endregion


            #region 3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.
            Console.WriteLine();
            Console.WriteLine("3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.");
            Console.WriteLine("------------------------------------------");



            var newproducts = ListGenerators.ProductList.Select(p => new
            {
                p.ProductName,
                Price = p.UnitPrice,
                p.UnitsInStock
            });


            foreach (var item in newproducts)
            {
                Console.WriteLine($"Name: {item.ProductName}, Price: {item.Price}, Stock: {item.UnitsInStock}");
            }
            #endregion

            #region 4. Determine if the value of int in an array match their position in the array.
            Console.WriteLine();
            Console.WriteLine("4. Determine if the value of int in an array match their position in the array.");
            Console.WriteLine("------------------------------------------");

            int[] positionArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };



            var position = positionArr.Where((num, index) => num == index);

            foreach (var item in position)
            {
                Console.WriteLine(item);
            }
            #endregion







            #endregion






            Console.ReadLine();
        }
    }
}
