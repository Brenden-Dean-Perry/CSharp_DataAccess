using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    internal class OpenAIMessageContentImageData
    {
        internal string url { get; }
        internal string detail { get; }

        /// <summary>
        /// Image data in OpenAI API format
        /// </summary>
        /// <param name="URL">Can be image URL or image converted to Base64 string.</param>
        /// <param name="Detail">Specifies the level of detail of the image.</param>
        public OpenAIMessageContentImageData(string image_data, string Detail = "auto")
        {
            url = image_data;
            detail = Detail;
        }
    }
}
