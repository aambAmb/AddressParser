using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressParser.Interfaces;

namespace AddressParser.Detectors
{
    public class HouseNameDetector : IDetector
    {
        private readonly Regex _regex =
            new Regex(@"\d.*(?<HouseNameChunk>\((?<HouseName>[\p{L}\p{Zs}\p{N}]+)\))",
                RegexOptions.IgnoreCase);

        public DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iteration)
        {
            var s = markedString.GetRestString();
            var match = _regex.Match(s);
            if (!match.Success) return null;

            var houseNameGroup = match.Groups["HouseName"];
            var houseNameChunk = match.Groups["HouseNameChunk"];

            var detectedAddress = new DetectedAddress(markedString)
            {
                HouseNamePosition = houseNameGroup.Index,
                HouseName = houseNameGroup.Value
            };

            detectedAddress.MarkedString.Mark(houseNameChunk.Index, houseNameChunk.Length);

            return detectedAddress;
        }
    }
}