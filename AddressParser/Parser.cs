using System.Collections.Generic;
using AddressParser.Detectors;
using AddressParser.Interfaces;

namespace AddressParser
{
    public class Parser : BaseAddressParser
    {
        protected override IEnumerable<IDetector> GetStreetDetectors()
        {
            return new IDetector[] {new StreetDetector()};
        }

        protected override IEnumerable<IDetector> GetRegionDetectors()
        {
            return new IDetector[] {new RegionDetector()};
        }

        protected override IEnumerable<IDetector> GetZipDetectors()
        {
            return new IDetector[] {new ZipDetector()};
        }

        protected override IEnumerable<IDetector> GetCityDetectors()
        {
            return new IDetector[] {new CityDetector()};
        }
    }
}