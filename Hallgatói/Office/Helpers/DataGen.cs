using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling_Office
{
    public static class DataGen
    {
        static readonly string[] Domains = { "example.com", "mail.org", "uni.edu", "service.net" };

        public static List<Customer> GenerateCustomers(int count, Random rnd)
        {
            var list = new List<Customer>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(new Customer
                {
                    Id = i + 1,
                    Name = $"Customer_{i}",
                    Email = $"user{i % 1000}@{Domains[rnd.Next(Domains.Length)]}"
                });
            }
            return list;
        }

        public static List<Order> GenerateOrders(int count, List<Customer> customers, Random rnd)
        {
            var list = new List<Order>(count);
            for (int i = 0; i < count; i++)
            {
                var c = customers[rnd.Next(customers.Count)];
                list.Add(new Order
                {
                    OrderId = i + 1,
                    CustomerId = c.Id,
                    Amount = rnd.Next(1, 500) + rnd.NextDouble(),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-rnd.Next(0, 60_000))
                });
            }
            return list;
        }

        public static string GenerateEmailsCsv(int rows, Random rnd)
        {
            var sb = new StringBuilder(rows * 20);
            for (int i = 0; i < rows; i++)
            {
                var user = $"user{rnd.Next(0, 50_000)}";
                var domain = i % 17 == 0 ? "bad_domain" : (i % 29 == 0 ? "mail..org" : "mail.org");
                sb.Append(user).Append('@').Append(domain).Append('\n');
            }
            return sb.ToString();
        }
    }
}
