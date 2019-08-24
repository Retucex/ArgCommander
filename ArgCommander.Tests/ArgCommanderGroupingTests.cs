using NUnit.Framework;
using static ArgCommander.Parser;

namespace ArgCommander.Tests
{
    [TestFixture]
    public class ArgCommanderGroupingTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [TestCase("-float", "3.14")]
        [TestCase("-float", "3.14", "-f", "-int", "5")]
        [TestCase("-float", "3.14", "-enum", "test1")]
        [TestCase("-float", "3.14", "-string", "hello")]
        [TestCase("-float", "3.14", "-char", "t")]
        public void Should_not_throw_grouping_exception(params string[] args)
        {
            var set = ParseArgs<TestGrouping>(args);
        }
    }
}
