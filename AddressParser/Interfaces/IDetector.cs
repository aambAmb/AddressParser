using System.Collections.Generic;

namespace AddressParser.Interfaces
{
    public interface IDetector
    {
        DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iterationNumber);
    }
}