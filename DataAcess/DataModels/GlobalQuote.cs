using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    //JSON deserialization and object instatiation only works for public fields. 
    //Make class internal so it is not accessable from outside the Data Access Layer.
    internal class GlobalQuote : APIDataModelRequest
    {
        [JsonPropertyName("01. symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("02. open")]
        public string? Open { get; set; }

        [JsonPropertyName("03. high")]
        public string? High { get; set; }

        [JsonPropertyName("04. low")]
        public string? Low { get; set; }

        [JsonPropertyName("05. price")]
        public string? Price { get; set; }

        [JsonPropertyName("06. volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("07. latest trading day")]
        public string? LatestTradingDay { get; set; }

        [JsonPropertyName("08. previous close")]
        public string? PreviousClose { get; set; }

        [JsonPropertyName("09. change")]
        public string? Change { get; set; }

    }
}
