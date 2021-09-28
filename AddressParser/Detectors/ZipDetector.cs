using System.Text.RegularExpressions;

namespace AddressParser.Detectors
{
    public class ZipDetector : RegexBasedDetector
    {
        public ZipDetector() : base(@"\b[1-9]\d{3}\b")
        {
        }

        protected override DetectedAddress Success(Match match, DetectedAddress detectedAddress)
        {
            detectedAddress.ZipCodePosition = match.Index;
            detectedAddress.ZipCode = match.Value;
            detectedAddress.MarkedString.Mark(match.Index, match.Length);
            return detectedAddress;
        }
    }
}