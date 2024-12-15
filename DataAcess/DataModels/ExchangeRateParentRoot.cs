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
    internal class ExchangeRateParentRoot : APIDataModelRequest
    {
        [JsonPropertyName("Realtime Currency Exchange Rate")]
        public ExchangeRateQuote ExchangeRateQuote { get; set; } = new ExchangeRateQuote();

    }
}
