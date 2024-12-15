using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataStructures
{
    internal class OpenAIMessageContentAudio : OpenAIMessageContent
    {
        internal OpenAIMessageContentAudioData input_audio { get; }
        internal OpenAIMessageContentAudio()
        {
            type = "input_audio";
        }
    }
}
