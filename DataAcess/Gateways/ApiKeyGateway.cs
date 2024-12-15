using DataAccessInterface;
using DataAccessInterface.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Gateways
{
    public class ApiKeyGateway : IGetApiKeys
    {
        private IConfig _configManager { get; set; }
        public ApiKeyGateway(IConfig configManager)
        {
            _configManager = configManager;
        }

        public Dictionary<string, string> GetData()
        {
            Classes.TextFileAPI textFileAPI = new Classes.TextFileAPI();
            string FilePath = _configManager.GetApiKeyDictonaryPath();
            string[] FileData = textFileAPI.LoadData(FilePath);

            Dictionary<string, string> Data = new Dictionary<string, string>();
            foreach (string lineData in FileData)
            {
                string[] splitdata = lineData.Split(",");
                Data.Add(splitdata[0].Trim(), splitdata[1].Trim());
            }
            return Data;
        }
    }
}
