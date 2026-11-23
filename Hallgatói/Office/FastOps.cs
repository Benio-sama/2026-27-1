using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Profiling_Office
{
    public static class FastOps
    {
        public static List<string> DistinctEmails(string csv) => SlowOps.DistinctEmailsSlow(csv);
        public static string BuildCsv(List<Order> orders) => SlowOps.BuildCsvSlow(orders);
        public static Dictionary<int, double> TotalByCustomer(List<Order> orders) => SlowOps.TotalByCustomerSlow(orders);
        public static int ValidateEmails(string csv) => SlowOps.ValidateEmailsSlow(csv);
    }
}
