using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CmdArgAttribute : Attribute
    {
        public CmdArgAttribute(string argument, CmdArgMode mode)
        {
            Argument = argument;
			Mode = mode;
        }
        public string Argument { get; }
        public CmdArgMode Mode { get; }
    }
}
