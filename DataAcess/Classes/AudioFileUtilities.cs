using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    internal static class AudioFileUtilities
    {
        /// <summary>
        /// Returns audio file content as string which can be used for transcription.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        internal static string GetAudioFileData(string FilePath)
        {
            byte[] fileData = System.IO.File.ReadAllBytes(FilePath);
            return System.Convert.ToBase64String(fileData);
        }
    }
}
