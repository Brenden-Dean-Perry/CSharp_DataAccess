using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    public class BBGAPIHistoricalDataPointStructure
    {
        public string Ticker { get; set; } = String.Empty;
        public string Field { get; set; } = String.Empty;
        public SortedDictionary<DateTime, decimal> Data { get; set; } = new SortedDictionary<DateTime, decimal>();
        public string Message { get; set; } = String.Empty;

        public BBGAPIHistoricalDataPointStructure()
        {
            Data = new SortedDictionary<DateTime, decimal>();
        }
    }
}
