using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressParser.Interfaces;

namespace AddressParser.Detectors
{
    public class RegionDetector : IDetector
    {
        private readonly Regex _regex = new Regex(@"[,\.\s]*(?<Region>(?:[A-z]|\d|\s){3,100})(?:,|$)",
            RegexOptions.IgnoreCase);

        public DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iteration)
        {
            var s = markedString.GetRestString();
            var match = _regex.Match(s);
            if (!match.Success) return null;

            var detectedAddress = new DetectedAddress(markedString)
            {
                RegionPosition = match.Groups["Region"].Index,
                Region = match.Groups["Region"].Value
            };
            detectedAddress.MarkedString.Mark(match.Groups["Region"].Index, match.Groups["Region"].Length);

            return detectedAddress;
        }
    }
}