using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloomberglp.Blpapi;
using DataAccess.DataStructures;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class BBGAPI : IBBGAPI
    {
        private static readonly Name SECURITY_DATA = Name.GetName("securityData");
        private static readonly Name SECURITY_NAME = Name.GetName("security");
        private static readonly Name FIELD_DATA = Name.GetName("fieldData");

        private Session _session { get; set; }
        private bool _sessionStarted { get; set; }
        private Service _dataService { get; set; }
        private Message _message { get; set; }
        private Request _request { get; set; }
        private StringBuilder _BDPMessage { get; set; } = new StringBuilder();
        private StringBuilder _BDHMessage { get; set; } = new StringBuilder();
        public BBGAPI()
        {
            StartSession();
        }

        public void StartSession()
        {
            string serverHost = "localhost";
            int serverPort = 8194;

            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.ServerHost = serverHost;
            sessionOptions.ServerPort = serverPort;
            sessionOptions.NumStartAttempts = 1;
            _session = new Session(sessionOptions);
            _sessionStarted = _session.Start();

            if (_sessionStarted)
            {
                string serviceURL = "//blp/refdata";
                bool serviceOpen = _session.OpenService(serviceURL);
                _dataService = _session.GetService(serviceURL);
                _sessionStarted = serviceOpen;
            }
        }

        public void KillConnection()
        {
            _session.Stop();
        }

        public bool IsBloombergConnected()
        {
            return _sessionStarted;
        }

        public string GetBDHAPIMessage()
        {
            return _BDHMessage.ToString();
        }

        public string GetBDPAPIMessage()
        {
            return _BDPMessage.ToString();
        }

        public BBGAPIDataPoint BDP(string Security, string BBGField)
        {
            string[] Securities = { Security };
            string[] BBGFields = { BBGField };
            return BDP(Securities, BBGFields);
        }

        public BBGAPIDataPoint BDP(string[] Securities, string BBGField)
        {
            string[] BBGFields = { BBGField };
            return BDP(Securities, BBGFields);
        }

        public BBGAPIDataPoint BDP(string[] Securities, string[] BBGFields)
        {
            BuildReferenceDataRequest(Securities, BBGFields);
            _session.SendRequest(_request, null);
            return GetDataPointFromAPIResponse();
        }

        private BBGAPIDataPoint GetDataPointFromAPIResponse()
        {
            _BDPMessage.Clear();
            BBGAPIDataPoint results = new BBGAPIDataPoint();

            while (true)
            {
                Event eventObj = _session.NextEvent();
                foreach (Message msg in eventObj)
                {
                    _message = msg;
                    _BDPMessage.Append(msg.ToString());
                    results.Add(DeserializeResponseLoopToResult());
                }

                if (eventObj.Type == Event.EventType.RESPONSE)
                {
                    break;
                }
            }
            return results;
        }

        public BBGAPIHistoricalDataPoint BDH(string Security, string BBGField, DateTime StartDate, DateTime EndDate, bool FillMissingValues = false)
        {
            _BDHMessage.Clear();
            BuildHistoricalDataRequest(Security, BBGField, StartDate, EndDate, FillMissingValues);
            _session.SendRequest(_request, null);
            BBGAPIHistoricalDataPoint results = new BBGAPIHistoricalDataPoint();

            while (true)
            {
                Event eventObj = _session.NextEvent();
                foreach (Message msg in eventObj)
                {
                    _message = msg;
                    _BDHMessage.Append(msg.ToString());
                    results.Add(DeserializeResponseToHistoricalResult(BBGField));
                }

                if (eventObj.Type == Event.EventType.RESPONSE)
                {
                    break;
                }
            }
            return results;
        }


        private BBGAPIDataPoint DeserializeResponseLoopToResult()
        {
            BBGAPIDataPoint results = new BBGAPIDataPoint();
            try
            {
                results = DeserializeResponseLoop();
            }
            catch { }
            return results;
        }

        private BBGAPIHistoricalDataPoint DeserializeResponseToHistoricalResult(string TargetField)
        {
            BBGAPIHistoricalDataPoint results = new BBGAPIHistoricalDataPoint();
            try
            {
                results = DeserializeHistoricalResponse(TargetField);
            }
            catch { }
            return results;
        }

        private BBGAPIDataPoint DeserializeResponseLoop()
        {
            BBGAPIDataPoint results = new BBGAPIDataPoint();

            Element securityData = GetSecurityDataElement();
            List<Element> SecurityDataElements = GetSubValuesAsElementsFromElement(securityData);
            foreach (Element securityDataElement in SecurityDataElements)
            {
                List<Element> fieldDataElements = GetSubElementsFromElement(securityDataElement);
                foreach (Element fieldDataElement in fieldDataElements)
                {
                    List<Element> fields = GetSubElementsFromElement(fieldDataElement);
                    foreach (Element field in fields)
                    {
                        BBGAPIDataPointStructure result = new BBGAPIDataPointStructure();
                        result.Ticker = GetSecurityNameFromElement(securityDataElement);
                        result.Message = _message.ToString();
                        result.Field = field.Name.ToString();
                        result.Value = field.GetValueAsString();
                        results.Add(result);
                    }
                }
            }

            return results;
        }


        private BBGAPIHistoricalDataPoint DeserializeHistoricalResponse(string TargetField)
        {
            BBGAPIHistoricalDataPoint results = new BBGAPIHistoricalDataPoint();
            BBGAPIHistoricalDataPointStructure result = new BBGAPIHistoricalDataPointStructure();

            Element securityData = GetSecurityDataElement();
            List<Element> SecurityDataElements = GetSubElementsFromElement(securityData);
            List<Element> FieldDataElements = GetSubValuesAsElementsFromMultipleElements(SecurityDataElements);
            List<Element> fields = GetSubElementsFromMultipleElements(FieldDataElements);

            result.Ticker = GetSecurityNameFromElement(securityData);
            result.Field = TargetField;
            result.Message = _message.ToString();
            result.Data = new SortedDictionary<DateTime, decimal>();
            DateTime DataDate = DateTime.MinValue;
            foreach (Element field in fields)
            {
                if (field.Name.ToString().ToLower() == "date")
                {
                    DataDate = field.GetValueAsDatetime().ToSystemDateTime();
                }

                if (field.Name.ToString().ToLower() == TargetField.ToLower())
                {
                    decimal returnValue = (decimal)field.GetValueAsFloat64();
                    result.Data.Add(DataDate, returnValue);
                }
            }
            results.Add(result);
            return results;
        }

        private Element GetSecurityDataElement()
        {
            return _message.GetElement(SECURITY_DATA);
        }

        private string GetSecurityNameFromElement(Element element)
        {
            if (element.HasElement(SECURITY_NAME))
            {
                return element.GetElementAsString(SECURITY_NAME);
            }
            return String.Empty;
        }


        private List<Element> GetSubElementsFromElement(Element element)
        {
            List<Element> subElements = new List<Element>();
            for (int i = 0; i < element.NumElements; i++)
            {
                subElements.Add(element.GetElement(i));
            }
            return subElements;
        }

        private List<Element> GetSubElementsFromMultipleElements(List<Element> elements)
        {
            List<Element> SubElements = new List<Element>();
            foreach (Element element in elements)
            {
                SubElements.AddRange(GetSubElementsFromElement(element));
            }
            return SubElements;
        }


        private List<Element> GetSubValuesAsElementsFromMultipleElements(List<Element> elements)
        {
            List<Element> subElements = new List<Element>();
            foreach (Element element in elements)
            {
                subElements.AddRange(GetSubValuesAsElementsFromElement(element));
            }
            return subElements;
        }

        private List<Element> GetSubValuesAsElementsFromElement(Element element)
        {
            List<Element> Elements = new List<Element>();
            for (int i = 0; i < element.NumValues; i++)
            {
                Elements.Add(element.GetValueAsElement(i));
            }
            return Elements;
        }

        private void BuildReferenceDataRequest(string Security, string BBGField)
        {
            string[] Securities = { Security };
            string[] Fields = { BBGField };
            BuildReferenceDataRequest(Securities, Fields);
        }


        private void BuildReferenceDataRequest(string[] Securities, string[] BBGFields)
        {
            _request = _dataService.CreateRequest("ReferenceDataRequest");
            foreach (string Security in Securities)
            {
                AppendSecurityToRequest(Security);
            }

            foreach (string field in BBGFields)
            {
                AppendFieldToRequest(field);
            }
        }


        private void BuildHistoricalDataRequest(string Security, string BBGField, DateTime StartDate, DateTime EndDate, bool FillMissingValues = false)
        {
            _request = _dataService.CreateRequest("HistoricalDataRequest");
            AppendSecurityToRequest(Security);
            AppendFieldToRequest(BBGField);
            AppendStartDateToRequest(StartDate);
            AppendEndDateToRequest(EndDate);

            if (FillMissingValues == true)
            {
                AppendFillMethodToRequest();
            }
        }

        private void AppendSecurityToRequest(string Security)
        {
            Name name = Name.GetName("securities");
            Element securities = _request.GetElement(name);
            securities.AppendValue(Security);
        }

        private void AppendFieldToRequest(string BBGField)
        {
            Name FIELDS = Name.GetName("fields");
            Element fieldElement = _request.GetElement(FIELDS);
            fieldElement.AppendValue(BBGField);
        }

        private void AppendFillMethodToRequest()
        {
            Name FillMethod = Name.GetName("nonTradingDayFillMethod");
            _request.Set(FillMethod, "PREVIOUS_VALUE");

            Name FillOption = Name.GetName("nonTradingDayFillOption");
            _request.Set(FillOption, "ALL_CALENDAR_DAYS");
        }


        private void AppendStartDateToRequest(DateTime StartDate)
        {
            Name name = Name.GetName("startDate");
            _request.Set(name, StartDate.ToString("yyyyMMdd"));
        }

        private void AppendEndDateToRequest(DateTime EndDate)
        {
            Name name = Name.GetName("endDate");
            _request.Set(name, EndDate.ToString("yyyyMMdd"));
        }

    }
}
