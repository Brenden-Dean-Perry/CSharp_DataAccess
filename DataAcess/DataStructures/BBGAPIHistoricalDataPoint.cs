using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    public class BBGAPIHistoricalDataPoint
    {
        private List<BBGAPIHistoricalDataPointStructure> _DataPoints { get; set; }
        public BBGAPIHistoricalDataPoint()
        {
            _DataPoints = new List<BBGAPIHistoricalDataPointStructure>();
        }

        public void Add(BBGAPIHistoricalDataPointStructure DataPoint)
        {
            _DataPoints.Add(DataPoint);
        }

        public void Add(BBGAPIHistoricalDataPoint DataPoints)
        {
            foreach (BBGAPIHistoricalDataPointStructure DataPoint in DataPoints._DataPoints)
            {
                _DataPoints.Add(DataPoint);
            }
        }

        public decimal GetValue(string Ticker, string Field, DateTime dateTime)
        {
            SortedDictionary<DateTime, decimal> values = new SortedDictionary<DateTime, decimal>();
            values = GetValueAsDictionary(Ticker, Field);
            decimal result = values[dateTime];
            return result;
        }

        public SortedDictionary<DateTime, decimal> GetValueTimeSeries(string Ticker, string Field)
        {
            SortedDictionary<DateTime, decimal> values = new SortedDictionary<DateTime, decimal>();
            return GetValueAsDictionary(Ticker, Field);
        }

        public int Count()
        {
            return _DataPoints.Count();
        }

        public string GetMessage()
        {
            return _DataPoints[0].Ticker.ToString() + " " + _DataPoints[0].Field.ToString() + " | " + _DataPoints[0].Message;
        }

        private SortedDictionary<DateTime, decimal> GetValueAsDictionary(string Ticker, string Field)
        {
            SortedDictionary<DateTime, decimal> values = new SortedDictionary<DateTime, decimal>();

            if(_DataPoints.Where(x => x.Ticker == Ticker && x.Field == Field).FirstOrDefault() != null)
            {
                values = _DataPoints.Where(x => x.Ticker ==Ticker && x.Field == Field).FirstOrDefault().Data;
            }
            return values;
        }
    }
}
