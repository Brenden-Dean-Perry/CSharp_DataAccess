using DataAccess.DataStructures;
using System;

namespace DataAccess.Interfaces
{
    public interface IBBGAPI
    {
        public BBGAPIHistoricalDataPoint BDH(string Security, string BBGField, DateTime StartDate, DateTime EndDate, bool FillMissingValues = false);
        public BBGAPIDataPoint BDP(string Security, string BBGField);
        public BBGAPIDataPoint BDP(string[] Securities, string BBGField);
        public BBGAPIDataPoint BDP(string[] Securities, string[] BBGFields);
        public string GetBDHAPIMessage();
        public string GetBDPAPIMessage();
        public bool IsBloombergConnected();
        public void KillConnection();
        public void StartSession();
    }
}