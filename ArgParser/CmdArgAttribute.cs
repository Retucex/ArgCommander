using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class CmdArgAttribute : Attribute
	{
		public string Argument { get; }
		public bool IsRequired { get; }
		public string Group { get; }
		public CmdArgGroupMode GroupMode { get; }

		/// <summary>
		/// Makes the property targetable by ArgParser.ParseArgs<T>() to receive the value or flag passed as a command line argument.
		/// </summary>
		/// <param name="argument">String passed by command line argument representing a flag or option</param>
		/// <param name="mode">Mode of the argument. Flag (bool) or Value</param>
		public CmdArgAttribute(string argument, bool isRequired, string group, CmdArgGroupMode groupMode)
        {
            Argument = argument;
			IsRequired = isRequired;
			Group = group;
			GroupMode = groupMode;
        }

		public CmdArgAttribute(string argument)
			: this(argument, false, null, CmdArgGroupMode.None)
		{

		}

	}
}
