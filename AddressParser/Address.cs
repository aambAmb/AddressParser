namespace AddressParser
{
    public class ParsedAddress
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Apartment { get; set; }
        public string ZipCode { get; set; }
        public string HouseName { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            return
                $"Zipcode:{ZipCode}, City:{City}, Region:{Region}, Street: {Street}, HouseNumber: {HouseNumber} ({HouseName}), Apartment:{Apartment}";
        }
    }
}