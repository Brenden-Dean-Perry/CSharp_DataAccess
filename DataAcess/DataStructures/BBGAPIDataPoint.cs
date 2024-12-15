using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    public class BBGAPIDataPoint
    {
        private List<BBGAPIDataPointStructure> _DataPoints { get; set; }
        public BBGAPIDataPoint()
        {
            _DataPoints = new List<BBGAPIDataPointStructure>();
        }

        public void Add(BBGAPIDataPointStructure DataPoint)
        {
            _DataPoints.Add(DataPoint);
        }

        public void Add(BBGAPIDataPoint DataPoints)
        {
            foreach(BBGAPIDataPointStructure DataPoint in _DataPoints)
            {
                _DataPoints.Add(DataPoint);
            }
        }

        public string GetValue(string Ticker, string Field)
        {
            BBGAPIDataPointStructure result = _DataPoints.Where(x => x.Ticker == Ticker && x.Field == Field).FirstOrDefault();
            if(result == null)
            {
                result = new BBGAPIDataPointStructure();
            }
            return result.Value;
        }

        public int Count()
        {
            return _DataPoints.Count();
        }

        public string GetMessage()
        {
            return _DataPoints[0].Message;
        }
    }
}
