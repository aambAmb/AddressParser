namespace AddressParser.Interfaces
{
    public interface IAddressParser
    {
        ParsedAddress Parse(string line);
    }
}