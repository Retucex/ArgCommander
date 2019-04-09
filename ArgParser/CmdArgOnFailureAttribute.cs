using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser
{
	[AttributeUsage(AttributeTargets.Method)]
	public class CmdArgOnFailureAttribute : Attribute
	{
	}
}
