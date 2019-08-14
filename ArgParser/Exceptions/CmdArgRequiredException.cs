using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ArgParser.Exceptions
{
    [Serializable]
    public class CmdArgRequiredException : CmdArgException
    {
        const string DefaultMessage = "Required argument was not supplied";

        public CmdArgRequiredException(string argument) : base(argument, $"{DefaultMessage} {argument}.") { }
        public CmdArgRequiredException(string argument, string message) : base(argument, message) { }
        public CmdArgRequiredException(string argument, string message, Exception innerException) : base(argument, message, innerException) { }
        protected CmdArgRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
