using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    internal class OpenAIMessageContentAudioData
    {
        internal string data { get; }
        internal string format { get; } = "mp3";

        /// <summary>
        /// Audio data in OpenAI API format
        /// </summary>
        /// <param name="data">Audio data converted to Base64 string.</param>
        public OpenAIMessageContentAudioData(string Data)
        {
            data = Data;
        }
    }
}
