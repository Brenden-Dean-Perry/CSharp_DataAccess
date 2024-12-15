using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    
    internal class MostActiveParentRoot : APIDataModelRequest
    {
        [JsonPropertyName("metadata")]
        public string? Metadata { get; set; }

        [JsonPropertyName("last_updated")]
        public string? LastUpdated { get; set; }

        [JsonPropertyName("top_gainers")]
        public List<TopGainer> TopGainers { get; set; } = new List<TopGainer>();

        [JsonPropertyName("top_losers")]
        public List<TopLoser> TopLosers { get; set; } = new List<TopLoser>();

        [JsonPropertyName("most_actively_traded")]
        public List<MostActivelyTraded> MostActivelyTraded { get; set; } = new List<MostActivelyTraded>();
    }
}
