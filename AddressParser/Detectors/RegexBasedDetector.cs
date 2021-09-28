using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressParser.Interfaces;

namespace AddressParser.Detectors
{
    public abstract class RegexBasedDetector : IDetector
    {
        private readonly Regex _regex;

        protected RegexBasedDetector(string regexPattern)
        {
            _regex = new Regex(regexPattern);
        }

        public DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iteration)
        {
            var detectedAddress = new DetectedAddress(markedString);
            var s = markedString.GetRestString();
            var match = _regex.Match(s);
            if (!match.Success) return null;

            return Success(match, detectedAddress);
        }

        protected abstract DetectedAddress Success(Match match, DetectedAddress detectedAddress);
    }
}