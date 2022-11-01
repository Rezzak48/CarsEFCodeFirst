using CarsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace CarsEF
{
    static class Operation
    {
        //Dislay helper
        public static void ToConsole<T>(this IEnumerable<T> input, string str)
        {
            Console.WriteLine($" ****** BEGIN {str} ****** ");
            foreach (var item in input) Console.WriteLine(item);
            Console.WriteLine($" ****** END {str} ****** ");
            Console.ReadLine();
        }
    }
    internal class Program
    {
        // disconnect to the database and remove the connection if you are using code first approach
        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory (relative path)|
        // ;Integrated Security=True;
        // add also : MultipleActiveResultSets = true
        static void Main(string[] args)
        {
            // Added EF : core, proxis, tool, sqlserver
            // Add The DB // change Build to content & copy always

            CarDbContext ctx = new CarDbContext();
            ctx.Brands.Select(x => x.Name).ToConsole("Brands");
            ctx.Cars.Select(x => $"{ x.Model} from {x.Brand.Name}").ToConsole("Cars");

            var q = from car in ctx.Cars
                    group car by new { car.BrandId, car.Brand.Name } into grp
                    select new
                    {
                        Brand =  grp.Key,
                        AveragePrice = grp.Average(x => x.BestPrice)
                    };
            q.ToConsole("AVG Prices");
        }
    }
}
