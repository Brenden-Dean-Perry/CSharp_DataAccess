using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.DataStructures;

namespace DataAccess
{
    public class BBGGateway
    {
        private BBGAPI _BBGAPI { get; set; }

        public BBGGateway(BBGAPI API)
        {
            _BBGAPI = API;
        }

        public void OpenConnection()
        {
            _BBGAPI.StartSession();
        }

        public bool IsDataFeedConnected()
        {
            return _BBGAPI.IsBloombergConnected();
        }

        public void CloseConnection()
        {
            _BBGAPI.KillConnection();
        }

        public string GetDataPoint(string Ticker, string Field)
        {
            BBGAPIDataPoint results = _BBGAPI.BDP(Ticker, Field);
            return results.GetValue(Ticker, Field);
        }

        public List<APIResponse> GetDataPoints(string[] Tickers, string Field)
        {
            List<APIResponse> values = new List<APIResponse>();
            BBGAPIDataPoint apiResults = _BBGAPI.BDP(Tickers, Field);
            foreach(string ticker in Tickers)
            {
                APIResponse result = new APIResponse();
                result.Ticker = ticker;
                result.Field = Field;
                result.Value = apiResults.GetValue(ticker, Field);
                values.Add(result);
            }
            return values;
        }

        public List<APIResponse> GetDataPoints(string[] Tickers, string[] Fields)
        {
            List<APIResponse> values = new List<APIResponse>();
            BBGAPIDataPoint apiResults = _BBGAPI.BDP(Tickers, Fields);
            foreach (string ticker in Tickers)
            {
                foreach(string field in Fields)
                {
                    APIResponse result = new APIResponse();
                    result.Ticker = ticker;
                    result.Field = field;
                    result.Value = apiResults.GetValue(ticker, field);
                    values.Add(result);
                }
            }
            return values;
        }

        public decimal GetHistoricalPoint(string Ticker, string Field, DateTime Date, bool FillPrices = false)
        {
            BBGAPIHistoricalDataPoint results = _BBGAPI.BDH(Ticker, Field, Date, Date, FillPrices);
            return results.GetValue(Ticker, Field, Date);
        }
        public SortedDictionary<DateTime,decimal> GetHistoricalPoints(string Ticker, string Field, DateTime StartDate, DateTime EndDate,  bool FillPrices = false)
        {
            BBGAPIHistoricalDataPoint results = _BBGAPI.BDH(Ticker, Field, StartDate, EndDate, FillPrices);
            return results.GetValueTimeSeries(Ticker, Field);
        }

        public string GetDataPointMessage(string Ticker, string Field)
        {
            BBGAPIDataPoint results = _BBGAPI.BDP(Ticker, Field);
            return _BBGAPI.GetBDPAPIMessage();
        }

        public string GetHistoricalDataPointMessage(string Ticker, string Field, DateTime Date, bool FillPrices = false)
        {
            BBGAPIHistoricalDataPoint results = _BBGAPI.BDH(Ticker, Field, Date, Date, FillPrices);
            return _BBGAPI.GetBDHAPIMessage();
        }

        public string GetHistoricalDataPointsMessage(string Ticker, string Field, DateTime StartDate, DateTime EndDate, bool FillPrices = false)
        {
            BBGAPIHistoricalDataPoint results = _BBGAPI.BDH(Ticker, Field, StartDate, EndDate, FillPrices);
            return _BBGAPI.GetBDHAPIMessage();
        }

        public string GetAPIResponseStatus(string APIMessage)
        {
            if(APIMessage.ToLower().Contains("invalid") == true)
            {
                return "Invalid Reponse";
            }
            return "Response OK";
        }
    }
}
