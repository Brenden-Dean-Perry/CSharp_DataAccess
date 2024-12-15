using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    public class BBGAPIDataPointStructure
    {
        public string Ticker { get; set; } = String.Empty;
        public string Field { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
    }
}
