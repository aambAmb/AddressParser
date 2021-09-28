using System.Collections.Generic;

namespace AddressParser.Detectors
{
    public class CityDetector : BaseDictionaryDetector
    {
        protected override bool Precondition(IEnumerable<DetectedAddress> detects, int iteration)
        {
            return iteration == 0;
        }

        protected override IDictionary<string, IEnumerable<string>> GetDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>
            {
                {"Lemesos", new List<string> {"Lemesos", "Limessos", "Lemessol", "Lemessos", "Llemesos"}},
                {"Paphos", new List<string> {"Paphos", "Pafos"}},
                {"Nicosia", new List<string> {"Nicosia", "Nicasia"}}
            };
        }

        protected override DetectedAddress Success(MarkedString markedString, int index, int length, string city)
        {
            var detectedAddress = new DetectedAddress(markedString)
            {
                CityPosition = index,
                City = city
            };
            detectedAddress.MarkedString.Mark(index, length);
            return detectedAddress;
        }
    }
}