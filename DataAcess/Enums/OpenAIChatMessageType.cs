using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataAccess.Enums
{
    internal enum OpenAIChatMessageType
    {
        [Description("text")]
        Text,

        [Description("image_url")]
        Image_URL,

        [Description("input_audio")]
        Input_Audio
    }
}
