using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CmdArgAttribute : Attribute
    {
		/// <summary>
		/// Makes the property targetable by ArgParser.ParseArgs<T>() to receive the value or flag passed as a command line argument.
		/// </summary>
		/// <param name="argument">String passed by command line argument representing a flag or option</param>
		/// <param name="mode">Mode of the argument. Flag (bool) or Value</param>
        public CmdArgAttribute(string argument, CmdArgMode mode)
        {
            Argument = argument;
			Mode = mode;
        }
        public string Argument { get; }
        public CmdArgMode Mode { get; }
    }
}
