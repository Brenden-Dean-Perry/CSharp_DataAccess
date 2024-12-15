using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccess.DataStructures
{
    public class OpenAIMessage<T>
    {
        public string role { get;}
        public T[] content { get;}

        public OpenAIMessage(OpenAIChatRole role, T[] content)
        {
            this.role = role.StringValueOf();
            this.content = content;
        }
    }
}
