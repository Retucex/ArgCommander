using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ArgParser
{
	public class CmdArgException : Exception
	{
		public CmdArgException() : base() { }
		public CmdArgException(string message) : base(message) { }
		public CmdArgException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		public CmdArgException(string message, Exception innerException) : base(message, innerException) { }
	}
}
