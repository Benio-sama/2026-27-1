using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Profiling_Office
{
    public static class SlowOps
    {
        // 1) O(n^2) distinct: list.Contains a belső ciklusban + sok GC
        public static List<string> DistinctEmailsSlow(string csv)
        {
            var lines = csv.Split('\n'); // allokáció minden sorra
            var result = new List<string>();
            foreach (var line in lines)
            {
                var trimmed = line.Trim(); // extra allokációk
                if (trimmed.Length == 0) continue;
                if (!result.Contains(trimmed)) // O(n) keresés
                    result.Add(trimmed);
            }
            return result;
        }

        // 2) String építés + fölösleges rendezések
        public static string BuildCsvSlow(List<Order> orders)
        {
            // Rossz: string konkat ciklusban + ToList + OrderBy többször
            var s = "";
            foreach (var o in orders.OrderBy(o => o.CustomerId).ToList())
            {
                s += o.OrderId + "," + o.CustomerId + "," + o.Amount.ToString("F2") + "," + o.CreatedAt.ToString("O") + "\n";
                // Fölösleges: minden iterációban dátumhoz .ToString("O") → boxing/format költség
            }
            return s;
        }

        // 3) Többszörös újra-Enumerate + ToList + Where lánc
        public static Dictionary<int, double> TotalByCustomerSlow(List<Order> orders)
        {
            var dict = new Dictionary<int, double>();
            var all = orders.ToList(); // felesleges másolat
            var customers = all.Select(o => o.CustomerId).Distinct().ToList();
            foreach (var c in customers)
            {
                var total = 0.0;
                foreach (var o in all.Where(x => x.CustomerId == c)) // minden ciklusban where-szűrés
                {
                    total += o.Amount;
                }
                if (!dict.ContainsKey(c)) dict.Add(c, total); else dict[c] = total;
            }
            return dict;
        }

        // 4) Regex újrafordítás minden hívásnál + túl bonyolult minta
        public static int ValidateEmailsSlow(string csv)
        {
            var lines = csv.Split('\n');
            int valid = 0;
            foreach (var line in lines)
            {
                var s = line.Trim();
                if (s.Length == 0) continue;
                // Rossz: minden iterációban új Regex; túl általános minta
                var re = new Regex(@"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$");
                if (re.IsMatch(s)) valid++;
            }
            return valid;
        }
    }
}
