using Profiling_Office;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfilingOffice
{
    class Program
    {
        static void Main(string[] args)
        {
            var seed = args.Length > 0 && int.TryParse(args[0], out var s) ? s : 42;
            var cfg = new WorkloadConfig
            {
                Customers = 50_000,
                Orders = 20_000,
                CsvRows = 50_000
            };

            Console.WriteLine($"Seed={seed}, Customers={cfg.Customers}, Orders={cfg.Orders}, CsvRows={cfg.CsvRows}");
            var rnd = new Random(seed);

            var customers = DataGen.GenerateCustomers(cfg.Customers, rnd);
            var orders = DataGen.GenerateOrders(cfg.Orders, customers, rnd);
            var csv = DataGen.GenerateEmailsCsv(cfg.CsvRows, rnd);

            SlowOps.DistinctEmailsSlow(csv);
            SlowOps.BuildCsvSlow(orders);
            SlowOps.TotalByCustomerSlow(orders);
            SlowOps.ValidateEmailsSlow(csv);

            Console.WriteLine("\n=== MÉRÉS (lassú implementációk) ===");
            Measure("DistinctEmailsSlow", () => SlowOps.DistinctEmailsSlow(csv));
            Measure("BuildCsvSlow", () => SlowOps.BuildCsvSlow(orders));
            Measure("TotalByCustomerSlow", () => SlowOps.TotalByCustomerSlow(orders));
            Measure("ValidateEmailsSlow", () => SlowOps.ValidateEmailsSlow(csv));

            Console.WriteLine("\nOptimalizált verzió:");

            Console.WriteLine("\n=== MÉRÉS (gyors implementációk) ===");
            Measure("DistinctEmailsFast", () => FastOps.DistinctEmails(csv));
            Measure("BuildCsvFast", () => FastOps.BuildCsv(orders));
            Measure("TotalByCustomerFast", () => FastOps.TotalByCustomer(orders));
            Measure("ValidateEmailsFast", () => FastOps.ValidateEmails(csv));

            Console.ReadKey();
        }

        static void Measure(string name, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine($"{name}: {sw.ElapsedMilliseconds} ms");
        }
    }




}