using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    //JSON deserialization and object instatiation only works for public fields. 
    //Make class internal so it is not accessable from outside the Data Access Layer.
    internal class GlobalMarketStatus
    {
        [JsonPropertyName("market_type")]
        public string? MarketType { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("primary_exchanges")]
        public string? PrimaryExchanges { get; set; }

        [JsonPropertyName("local_open")]
        public string? LocalOpen { get; set; }

        [JsonPropertyName("local_close")]
        public string? LocalClose { get; set; }

        [JsonPropertyName("current_status")]
        public string? CurrentStatus { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

    }
}
