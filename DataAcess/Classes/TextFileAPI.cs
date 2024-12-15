using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataAccessInterface;

namespace DataAccess.Classes
{
    public class TextFileAPI : IFileDataAccess
    {
        /// <summary>
        /// Gets data from text file and loads each line into a string array.
        /// </summary>
        /// <param name="Path">Text file path</param>
        /// <returns></returns>
        public string[] LoadData(string Path)
        {
            return File.ReadAllLines(Path);
        }

        /// <summary>
        /// Inserts content data into a text file. If the file does not exist, a new file will be created.
        /// </summary>
        /// <param name="Path">Text file path</param>
        /// <param name="Content"></param>
        public void InsertData(string Path, string[] Content)
        {
            File.AppendAllLines(Path, Content);
        }

    }
}
