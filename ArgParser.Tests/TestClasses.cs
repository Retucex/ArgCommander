using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ArgParser.Tests
{
	public enum TestEnum
	{
		test0,
		test1,
		test2
	}

	class TestConversion
	{
		[CmdArg("-f")]
		[CmdArg("-flag")]
		public bool Flag { get; set; }

		[CmdArg("-int")]
		public int IntVal { get; set; }

		[CmdArg("-float")]
		public float FloatVal { get; set; }

		[CmdArg("-double")]
		public double DoubleVal { get; set; }

		[CmdArg("-decimal")]
		public decimal DecimalVal { get; set; }

		[CmdArg("-enum")]
		public TestEnum EnumVal { get; set; }

		[CmdArg("-string")]
		public string StringVal { get; set; }

		[CmdArg("-char")]
		public char CharVal { get; set; }
	}

	class TestFailure
	{
		[CmdArg("-int")]
		public int floatInIntVal { get; set; }
	}

	class TestFailureCustom
	{
		[CmdArg("-int")]
		public int floatInIntVal { get; set; }

		[CmdArgOnFailure]
		public void CustomError()
		{
			throw new CmdArgException("Custom Error");
		}
	}
}
