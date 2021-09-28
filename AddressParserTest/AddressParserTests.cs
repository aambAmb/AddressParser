using System.Diagnostics;
using System.IO;
using System.Linq;
using AddressParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressParserTest
{
    [TestClass]
    public class AddressParserTests
    {
        private static readonly Parser _parser = new Parser();

        [TestMethod]
        [DeploymentItem("Addresses.txt")]
        public void DoParseTest()
        {
            var lines = File.ReadLines("Addresses.txt")
                .Select(i => i.Trim('"'))
                .Where(i => !string.IsNullOrWhiteSpace(i));

            foreach (var line in lines)
            {
                var address = _parser.Parse(line);
                Debug.WriteLine(address);
            }
        }

        [TestMethod]
        public void DoZipCodeTest()
        {
            Assert.AreEqual("4150", _parser.Parse("4150, Lemesos, Kato Polemidia, Vasileos Georgiou A,").ZipCode);
        }

        [TestMethod]
        public void DoCityTest()
        {
            Assert.AreEqual("Lemesos", _parser.Parse("4150, Lemesos, Kato Polemidia, Vasileos Georgiou A,").City);
        }

        [TestMethod]
        public void DoHouseNumberTest()
        {
            Assert.AreEqual("137b",
                _parser.Parse("3001, lemesos, Lemesos, Griva Digeni, 137b, Realas Building, Block B").HouseNumber);
        }

        [TestMethod]
        public void DoRegionTest()
        {
            Assert.AreEqual("Mesa Geitonia",
                _parser.Parse("4005, Lemesos, Mesa Geitonia, Dimitriou Mitropoulou, 18g").Region);
        }
    }
}