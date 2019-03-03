using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser
{
	/// <summary>
	/// Mode of operation for the CmdArg. Can be a flag (bool) or an option followed by a value.
	/// </summary>
	public enum CmdArgMode
	{
		IsFlag,
		HasValue
	}
}
