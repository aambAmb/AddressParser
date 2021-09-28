using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressParser.Interfaces;

namespace AddressParser.Detectors
{
    public class StreetDetector : IDetector
    {
        private readonly Regex _regex =
            new Regex(
                @"[,\.\s]*(?<Street>(?:[A-z]|\s|\d){3,100})\s?,\s(?<House>\d{1,3}-?[a-z]?)(?:\s?,\s(?<Apt>(?:\d*-?[A-Z|\d]*)))?",
                RegexOptions.IgnoreCase);

        public DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iteration)
        {
            var s = markedString.GetRestString();
            var match = _regex.Match(s);
            if (!match.Success) return null;

            var detectedAddress = new DetectedAddress(markedString)
            {
                StreetPosition = match.Groups["Street"].Index,
                Street = match.Groups["Street"].Value
            };
            detectedAddress.MarkedString.Mark(match.Groups["Street"].Index, match.Groups["Street"].Length);

            var houseGroup = match.Groups["House"];
            if (houseGroup != null)
            {
                detectedAddress.HouseNumberPosition = houseGroup.Index;
                detectedAddress.HouseNumber = houseGroup.Value;
                detectedAddress.MarkedString.Mark(houseGroup.Index, houseGroup.Length);
            }

            var aptGroup = match.Groups["Apt"];
            if (houseGroup != null)
            {
                detectedAddress.ApartmentPosition = aptGroup.Index;
                detectedAddress.Apartment = aptGroup.Value;
                detectedAddress.MarkedString.Mark(aptGroup.Index, aptGroup.Length);
            }

            return detectedAddress;
        }
    }
}