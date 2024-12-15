using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;
using Utilities;

namespace DataAccess.DataStructures
{
    internal class OpenAIMessageContentImage : OpenAIMessageContent
    {
        internal OpenAIMessageContentImageData image_url { get; }

        /// <summary>
        /// Message content of image in OpenAI format.
        /// </summary>
        /// <param name="image_data">Can be image URL or image converted to Base64 string.</param>
        /// <param name="imageType"></param>
        internal OpenAIMessageContentImage(string image_data, OpenAIContentImageType imageType = OpenAIContentImageType.Base64String)
        {
            type = OpenAIChatMessageType.Image_URL.StringValueOf();
            image_url = new OpenAIMessageContentImageData(image_data);
        }

    }
}
