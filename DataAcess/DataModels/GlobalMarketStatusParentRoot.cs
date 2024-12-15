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
    internal class GlobalMarketStatusParentRoot : APIDataModelRequest
    {
        [JsonPropertyName("endpoint")]
        public string? Endpoint { get; set; }

        [JsonPropertyName("markets")]
        public List<GlobalMarketStatus> MarketStatusResults { get; set; } = new List<GlobalMarketStatus>();

    }
}
