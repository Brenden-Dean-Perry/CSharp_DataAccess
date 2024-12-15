using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    
    internal class TopGainer
    {
        [JsonPropertyName("ticker")]
        public string? Ticker { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("change_amount")]
        public string? ChangeAmount { get; set; }

        [JsonPropertyName("change_percentage")]
        public string? ChangePercentage { get; set; }

        [JsonPropertyName("volume")]
        public string? Volume { get; set; }
    }

    

}
