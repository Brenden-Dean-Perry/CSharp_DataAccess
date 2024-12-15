using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.DataModels
{
    internal class SymbolSearchParentRoot : APIDataModelRequest
    {
        [JsonPropertyName("bestMatches")]
        public List<SymbolSearch> SymbolSearchResults { get; set; } = new List<SymbolSearch>();

    }
}
