using Cheeseria.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cheeseria.API.Data
{
    public class CheeseContextSeed
    {
        public static async Task SeedAsync(CheeseContext context)
        {
            if (!context.Cheeses.Any())
            {
                context.Cheeses.AddRange(
                    GetPreconfiguredCheesesFromFile());

                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<Cheese> GetPreconfiguredCheesesFromFile()
        {
            string csvFileCatalogItems = Path.Combine(Environment.CurrentDirectory, "Data", "Cheeses.csv");

            return File.ReadAllLines(csvFileCatalogItems)
                                   .Skip(1) // skip header row
                                   .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                                   .Select(column => new Cheese
                                   {
                                       Id = Convert.ToInt32(column[0]),
                                       Name = column[1],
                                       Description = column[2],
                                       Colour = column[3],
                                       Price = Convert.ToDecimal(column[4]),
                                       ImageUrl = column[5]
                                   })
                                   .Where(x => x != null);
        }
    }
}
