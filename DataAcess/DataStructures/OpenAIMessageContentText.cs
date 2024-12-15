using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccess.DataStructures
{
    public class OpenAIMessageContentText : OpenAIMessageContent
    {
        public string text { get; }
        /// <summary>
        /// Text message content in OpenAI API format.
        /// </summary>
        /// <param name="text"></param>
        public OpenAIMessageContentText(string text)
        {
            this.type = OpenAIChatMessageType.Text.StringValueOf();
            this.text = text;
        }
    }
}
