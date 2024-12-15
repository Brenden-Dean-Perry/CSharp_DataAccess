using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Utilities;

namespace DataAccess
{
    internal class InternalAlphaVantageAPI
    {
        private string _apiKey { get; set; }
        internal InternalAlphaVantageAPI(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal decimal GetPrice(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return decimal.Parse(quote.Price);
        }
        internal decimal GetPreviousClose(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return decimal.Parse(quote.PreviousClose);
        }

        internal decimal GetVolume(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return decimal.Parse(quote.Volume);
        }

        internal decimal GetHigh(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return decimal.Parse(quote.High);
        }

        internal decimal GetLow(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return decimal.Parse(quote.Low);
        }

        internal DateTime GetLatestTradingDay(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            return DateTime.Parse(quote.LatestTradingDay);
        }

        internal decimal GetChangePercent(string symbol)
        {
            DataModels.GlobalQuote quote = GetQuote(symbol);
            decimal price = decimal.Parse(quote.Price);
            decimal price_previous = decimal.Parse(quote.PreviousClose);
            return price / price_previous - 1;
        }

        internal decimal GetExchangeRate(string FromCurrencySymbol, string ToCurrencySymbol)
        {
            DataModels.ExchangeRateQuote quote = GetExchangeRateQuote(FromCurrencySymbol, ToCurrencySymbol);
            return decimal.Parse(quote.ExchangeRate);
        }

        private DataModels.GlobalQuote GetQuote(string symbol)
        {
            string URL = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";
            string result = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            DataModels.GlobalQuoteParentRoot quoteParentRoot = Utilities.JSONUtilities.JSONStringToObject<DataModels.GlobalQuoteParentRoot>(result);
            return quoteParentRoot.GlobalQuote;
        }

        private DataModels.ExchangeRateQuote GetExchangeRateQuote(string FromCurrencySymbol, string ToCurrencySymbol)
        {
            string URL = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={FromCurrencySymbol}&to_currency={ToCurrencySymbol}&apikey={_apiKey}";
            string result = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            DataModels.ExchangeRateParentRoot exchangeRateParentRoot = Utilities.JSONUtilities.JSONStringToObject<DataModels.ExchangeRateParentRoot>(result);
            return exchangeRateParentRoot.ExchangeRateQuote;
        }

        private List<DataModels.GlobalMarketStatus> GetGlobalMarketStatuses()
        {
            string URL = $"https://www.alphavantage.co/query?function=MARKET_STATUS&apikey={_apiKey}";
            string result = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            DataModels.GlobalMarketStatusParentRoot marketStatusRoot = Utilities.JSONUtilities.JSONStringToObject<DataModels.GlobalMarketStatusParentRoot>(result);
            return marketStatusRoot.MarketStatusResults;
        }

        private List<DataModels.SymbolSearch> GetTickerSearchResults(string text)
        {
            string URL = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={text}&apikey={_apiKey}";
            string result = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            DataModels.SymbolSearchParentRoot symbolSearchRoot = Utilities.JSONUtilities.JSONStringToObject<DataModels.SymbolSearchParentRoot>(result);
            return symbolSearchRoot.SymbolSearchResults;
        }

        private DataModels.MostActiveParentRoot GetMostActiveMovers()
        {
            string URL = $"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={_apiKey}";
            string result = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            DataModels.MostActiveParentRoot mostActive = Utilities.JSONUtilities.JSONStringToObject<DataModels.MostActiveParentRoot>(result);
            return mostActive;   
        }

        private SortedDictionary<DateTime, DataModels.TimeSeriesData> GetIntradayStockTimeSeries(string Symbol, Enums.AlphaVantageTimeInterval TimeInterval = Enums.AlphaVantageTimeInterval.FiveMin, Enums.AlphaVantageOutputSize OutputSize = Enums.AlphaVantageOutputSize.Compact)
        {
            string OutputSizeText = OutputSize.StringValueOf();
            string TimeIntervalText = TimeInterval.StringValueOf();
            string URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={Symbol}&interval={TimeIntervalText}&outputsize={OutputSizeText}&apikey={_apiKey}";
            string JSONString = Utilities.JSONUtilities.GetJSONStringFromAPIRequest(URL);
            //https://stackoverflow.com/questions/74860946/how-do-i-deserialize-json-that-has-date-based-property-names
            DataModels.TimeSeriesParentRoot timeSeries =  Utilities.JSONUtilities.JSONStringToObject<DataModels.TimeSeriesParentRoot>(JSONString);
            string timesSeriesJSON = timeSeries.TimeSeriesJSON;

            Dictionary<DateTime, string> timeSeresDict = JObject.Parse(timesSeriesJSON)["Time Series (5min)"].ToObject<Dictionary<DateTime, string>>();

            SortedDictionary<DateTime, DataModels.TimeSeriesData> data = new SortedDictionary<DateTime, DataModels.TimeSeriesData>();
            foreach (var pair in timeSeresDict)
            {
                DataModels.TimeSeriesData extractedData = Utilities.JSONUtilities.JSONStringToObject<DataModels.TimeSeriesData>(pair.Value);
                data.Add(pair.Key, extractedData);
            }

            return data;
        }

    }
}