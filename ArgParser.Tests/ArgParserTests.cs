using ArgParser;
using static ArgParser.ArgParser;
using NUnit.Framework;

namespace Tests
{
	public enum TestEnum
	{
		test0,
		test1,
		test2
	}

	class TestSettings
	{
		[CmdArg("-flag", CmdArgMode.IsFlag)]
		public bool Flag { get; set; }

		[CmdArg("-int", CmdArgMode.HasValue)]
		public int IntVal { get; set; }

		[CmdArg("-float", CmdArgMode.HasValue)]
		public float FloatVal { get; set; }

		[CmdArg("-double", CmdArgMode.HasValue)]
		public double DoubleVal { get; set; }

		[CmdArg("-decimal", CmdArgMode.HasValue)]
		public decimal DecimalVal { get; set; }

		[CmdArg("-enum", CmdArgMode.HasValue)]
		public TestEnum EnumVal { get; set; }

		[CmdArg("-string", CmdArgMode.HasValue)]
		public string StringVal { get; set; }

		[CmdArg("-char", CmdArgMode.HasValue)]
		public char CharVal { get; set; }
	}

	public class ArgParserTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Should_set_flag_to_true()
		{
			var args = new[] { "-flag" };
			var set = ParseArgs<TestSettings>(args);

			Assert.True(set.Flag, "Failed to set flag.");
		}

		[TestCase("1", ExpectedResult = 1)]
		[TestCase("10000", ExpectedResult = 10000)]
		[TestCase("-2", ExpectedResult = -2)]
		public int Should_set_int_to_value(string val)
		{
			var args = new[] { "-int", val };
			var set = ParseArgs<TestSettings>(args);

			return set.IntVal;
		}

		[TestCase("1", 1.0f)]
		[TestCase("1.56987", 1.56987f)]
		[TestCase("-2.5555", -2.5555f)]
		public void Should_set_float_to_value(string val, float expected)
		{
			var args = new[] { "-float", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, set.FloatVal, 0.000005);
		}

		[TestCase("1", 1.0)]
		[TestCase("1.56987", 1.56987)]
		[TestCase("-2.5555", -2.5555)]
		public void Should_set_double_to_value(string val, double expected)
		{
			var args = new[] { "-double", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, set.DoubleVal, 0.000005);
		}

		[TestCase("1", 1.0)]
		[TestCase("1.56987", 1.56987)]
		[TestCase("-2.5555", -2.5555)]
		public void Should_set_decimal_to_value(string val, double expected)
		{
			var args = new[] { "-decimal", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, (double)set.DecimalVal, 0.000005);
		}

		[TestCase("0", TestEnum.test0)]
		[TestCase("1", TestEnum.test1)]
		[TestCase("2", TestEnum.test2)]
		[TestCase("test0", TestEnum.test0)]
		[TestCase("test1", TestEnum.test1)]
		[TestCase("test2", TestEnum.test2)]
		public void Should_set_enum_to_value(string val, TestEnum expected)
		{
			var args = new[] { "-enum", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, set.EnumVal);
		}

		[TestCase(@"test", @"test")]
		[TestCase(@"\test", @"\test")]
		[TestCase(@"\ntest", @"\ntest")]
		[TestCase(@"!@#$%test///", @"!@#$%test///")]
		[TestCase("\"test with spaces\"", "\"test with spaces\"")]
		public void Should_set_string_to_value(string val, string expected)
		{
			var args = new[] { "-string", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, set.StringVal);
		}

		[TestCase("t", 't')]
		public void Should_set_char_to_value(string val, char expected)
		{
			var args = new[] { "-char", val };
			var set = ParseArgs<TestSettings>(args);

			Assert.AreEqual(expected, set.CharVal);
		}
	}
}