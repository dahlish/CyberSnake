using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;


namespace Practice5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var intRect = new Rectangle<int>(2, 5);
            var doubleRect = new Rectangle<double>(2.3, 4.2);

            Show(32);
            Show("Hello");

            Console.WriteLine();
            Console.WriteLine();

            var a = new Wrapper<int>(3);
            var b = new Wrapper<double>(5.6);
            var c = a;

            a.Show();
            b.Show();
            c.Set(5);
            a.Show();

            Stopwatch watch = new Stopwatch();


            List<int> lista = new List<int>();

            watch.Start();
            for (int i = 0; i < 100000000; i++)
            {
                lista.Add(i);
            }
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i] == 99999999)
                {
                    Console.WriteLine("Hurra!");
                    watch.Stop();
                }
            }
            Console.WriteLine("Took: " + (watch.ElapsedMilliseconds / 1000) + " seconds.");

            watch = new Stopwatch();

            HashSet<int> hashSet = new HashSet<int>();

            watch.Start();

            for (int i = 0; i < 100000000; i++)
            {
                hashSet.Add(i);
            }

            if (hashSet.Contains(99999999))
            {
                Console.WriteLine("Hurra hash!");
                watch.Stop();
            }
            Console.WriteLine("Took: " + (watch.ElapsedMilliseconds / 1000) + " seconds.");

            var customers = new[] {
                 new { CustomerID = 1, FirstName = "Kim", LastName = "Abercrombie",
                       CompanyName = "Alpine Ski House" },
                 new { CustomerID = 2, FirstName = "Jeff", LastName = "Hay",
                       CompanyName = "Coho Winery" },
                 new { CustomerID = 3, FirstName = "Charlie", LastName = "Herb",
                       CompanyName = "Alpine Ski House" },
                 new { CustomerID = 4, FirstName = "Chris", LastName = "Preston",
                       CompanyName = "Trey Research" },
                 new { CustomerID = 5, FirstName = "Dave", LastName = "Barnett",
                       CompanyName = "Wingtip Toys" },
                 new { CustomerID = 6, FirstName = "Ann", LastName = "Beebe",
                       CompanyName = "Coho Winery" },
                 new { CustomerID = 7, FirstName = "John", LastName = "Kane",
                       CompanyName = "Wingtip Toys" },
                 new { CustomerID = 8, FirstName = "David", LastName = "Simpson",
                       CompanyName = "Trey Research" },
                 new { CustomerID = 9, FirstName = "Greg", LastName = "Chapman",
                       CompanyName = "Wingtip Toys" },
                 new { CustomerID = 10, FirstName = "Tim", LastName = "Litton",
                       CompanyName = "Wide World Importers" }
            };
            var addresses = new[] {
                 new { CompanyName = "Alpine Ski House", City = "Berne",
                       Country = "Switzerland"},
                 new { CompanyName = "Coho Winery", City = "San Francisco",
                       Country = "United States"},
                 new { CompanyName = "Trey Research", City = "New York",
                       Country = "United States"},
                 new { CompanyName = "Wingtip Toys", City = "London",
                       Country = "United Kingdom"},
                 new { CompanyName = "Wide World Importers", City = "Tetbury",
                       Country = "United Kingdom"}
            };

            IEnumerable<string> customerNames = customers.OrderBy(cust => cust.LastName).Select(cust => $"{cust.FirstName} {cust.LastName}");
            IEnumerable<string> customerLastNames = from customer in customers select customer.LastName;

            foreach(var name in customerNames)
            {
                Console.WriteLine(name);
            }




        }

        public static void Show(object arg1)
        {
            if (arg1 is int)
            {
                Console.WriteLine("heltal");
            }
            else
            {
                Console.WriteLine(arg1.ToString());
            }
        }
    }
}
