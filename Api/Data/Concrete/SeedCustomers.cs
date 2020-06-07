using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Concrete.EntityFramework;
using Api.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;

namespace Api.Data.Concrete
{
    public class SeedCustomers
    {
        public static async Task SeedAsync(CustomerContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<SeedCustomers>();

            string dataPath = @"D:\Projects\ToyProject\Api\Data\SeedData\MOCK_CUSTOMERS.json";

            try
            {
                if (!context.Customers.Any())
                {
                    string stringCustomers = File.ReadAllText(dataPath);

                    List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(stringCustomers);

                    customers = customers.DistinctBy(c => c.Id).ToList();

                    foreach (var customer in customers)
                    {
                        context.Customers.Add(customer);
                    }

                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded customers");
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }

        }
    }
}