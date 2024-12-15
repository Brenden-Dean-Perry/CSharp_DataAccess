using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class TextFileLogger: ILog
    {
        private IFileDataAccess _fileAPI { get; set; }
        private IConfig _config {  get; set; }
        public TextFileLogger(IConfig config, IFileDataAccess fileDataAccess)
        {
            _config = config;
            _fileAPI = fileDataAccess;   
        }

        /// <summary>
        /// Logs exeception error message to log directory path designated in application config file.
        /// </summary>
        /// <param name="exception"></param>
        public void Log(Exception exception)
        {
            string logFilePath = String.Empty;
            string[] content = { $"Message: {exception.Message}", "", $"Source: {exception.Source}", "", $"Stack Trace: {exception.StackTrace}" };
            _fileAPI.InsertData(logFilePath, content);
        }
    }
}
