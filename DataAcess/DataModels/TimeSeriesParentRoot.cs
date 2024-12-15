using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    internal class TimeSeriesParentRoot
    {
        [JsonPropertyName("Meta Data")]
        public TimeSeriesMetaData MetaData { get; set; }

        [JsonPropertyName("Time Series (5min)")]
        public string? TimeSeriesJSON { get; set; }

        [JsonIgnore]
        public string Test { get; set; } = "5min";

    }
}
