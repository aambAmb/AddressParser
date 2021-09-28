namespace AddressParser
{
    public class DetectedAddress : ParsedAddress
    {
        public DetectedAddress(MarkedString markedString)
        {
            MarkedString = markedString;
        }

        public int? CityPosition { get; set; }
        public int? RegionPosition { get; set; }
        public int? StreetPosition { get; set; }
        public int? HouseNumberPosition { get; set; }
        public int? ZipCodePosition { get; set; }
        public int? ApartmentPosition { get; set; }
        public int? HouseNamePosition { get; set; }

        public MarkedString MarkedString { get; set; }
    }
}