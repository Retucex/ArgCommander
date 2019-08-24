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

    class TestGrouping
    {
        [CmdArg("-f", false, "group1", CmdArgGroupMode.All)]
        [CmdArg("-flag", false, "group1", CmdArgGroupMode.All)]
        public bool? Flag { get; set; }

        [CmdArg("-int", false, "group1", CmdArgGroupMode.All)]
        public int? IntVal { get; set; }

        [CmdArg("-float", false, "group2", CmdArgGroupMode.AtleastOne)]
        public float? FloatVal { get; set; }

        [CmdArg("-double", false, "group2", CmdArgGroupMode.AtleastOne)]
        public double? DoubleVal { get; set; }

        [CmdArg("-decimal", false, "group2", CmdArgGroupMode.AtleastOne)]
        public decimal? DecimalVal { get; set; }

        [CmdArg("-enum", false, "group3", CmdArgGroupMode.OnlyOne)]
        public TestEnum? EnumVal { get; set; }

        [CmdArg("-string", false, "group3", CmdArgGroupMode.OnlyOne)]
        public string StringVal { get; set; }

        [CmdArg("-char", false, "group3", CmdArgGroupMode.OnlyOne)]
        public char? CharVal { get; set; }
    }
}
