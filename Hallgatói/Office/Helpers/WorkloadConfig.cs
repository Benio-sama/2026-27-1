using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling_Office
{
    public class WorkloadConfig
    {
        public int Customers { get; init; }
        public int Orders { get; init; }
        public int CsvRows { get; init; }
    }
}
