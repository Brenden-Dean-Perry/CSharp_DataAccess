using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AlphaVantageAPI
    {
        private InternalAlphaVantageAPI api { get; set; }
        public AlphaVantageAPI(string ApiKey)
        {
            api = new InternalAlphaVantageAPI(ApiKey);
        }

        public decimal GetPrice(string Symbol)
        {
            return api.GetPrice(Symbol);
        }

        public decimal GetPricePreviousClose(string Symbol)
        {
            return api.GetPreviousClose(Symbol);
        }

        public decimal GetChangePercentage(string Symbol)
        {
            return api.GetChangePercent(Symbol);
        }

        public decimal GetVolume(string Symbol)
        {
            return api.GetVolume(Symbol);
        }


        public decimal GetPriceLow(string Symbol)
        {
            return api.GetLow(Symbol);
        }

        public decimal GetPriceHigh(string Symbol)
        {
            return api.GetHigh(Symbol);
        }

        public DateTime GetLatestTradingDay(string Symbol)
        {
            return api.GetLatestTradingDay(Symbol);
        }

    }
}
