using System.Collections.Generic;
using System.Linq;
using AddressParser.Interfaces;

namespace AddressParser
{
    public abstract class BaseAddressParser : IAddressParser
    {
        private readonly IList<IDetector> _detectors = new List<IDetector>();

        private bool _isInitialized;

        public ParsedAddress Parse(string line)
        {
            if (!_isInitialized)
            {
                Initialize();
                _isInitialized = true;
            }

            var detects = new List<DetectedAddress>(100);

            for (var i = 0; i < 2; i++)
            {
                var currentMarkedString = new MarkedString(line);
                foreach (var detector in _detectors)
                {
                    var detect = detector.Detect(currentMarkedString, detects, i);
                    if (detect != null)
                    {
                        detects.Add(detect);
                        currentMarkedString = detect.MarkedString;
                    }
                }
            }

            var city = detects.Where(i => !string.IsNullOrWhiteSpace(i.City))
                .GroupBy(i => i.City)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var zipCode = detects.Where(i => !string.IsNullOrWhiteSpace(i.ZipCode))
                .GroupBy(i => i.ZipCode)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var region = detects.Where(i => !string.IsNullOrWhiteSpace(i.Region))
                .GroupBy(i => i.Region)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var street = detects.Where(i => !string.IsNullOrWhiteSpace(i.Street))
                .GroupBy(i => i.Street)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var houseNumber = detects.Where(i => !string.IsNullOrWhiteSpace(i.HouseNumber))
                .GroupBy(i => i.HouseNumber)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var apartment = detects.Where(i => !string.IsNullOrWhiteSpace(i.Apartment))
                .GroupBy(i => i.Apartment)
                .OrderByDescending(g => g.Count())
                .Select(i => i.Key).FirstOrDefault();

            var address = new ParsedAddress
            {
                ZipCode = zipCode,
                City = city,
                Region = region,
                Street = street,
                HouseNumber = houseNumber,
                Apartment = apartment
            };

            return address;
        }

        private void Initialize()
        {
            AddDetectors(GetCityDetectors());
            AddDetectors(GetZipDetectors());
            AddDetectors(GetRegionDetectors());
            AddDetectors(GetStreetDetectors());
        }

        private void AddDetectors(IEnumerable<IDetector> detectors)
        {
            if (detectors == null) return;

            foreach (var detector in detectors) _detectors.Add(detector);
        }

        protected abstract IEnumerable<IDetector> GetStreetDetectors();
        protected abstract IEnumerable<IDetector> GetRegionDetectors();
        protected abstract IEnumerable<IDetector> GetZipDetectors();
        protected abstract IEnumerable<IDetector> GetCityDetectors();
    }
}