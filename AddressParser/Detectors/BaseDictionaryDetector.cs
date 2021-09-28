using System;
using System.Collections.Generic;
using AddressParser.Interfaces;

namespace AddressParser.Detectors
{
    public abstract class BaseDictionaryDetector : IDetector
    {
        public DetectedAddress Detect(MarkedString markedString, IEnumerable<DetectedAddress> detects, int iteration)
        {
            if (!Precondition(detects, iteration)) return null;

            var s = markedString.GetRestString();

            foreach (var entry in GetDictionary())
            foreach (var alias in entry.Value)
            {
                var index = s.IndexOf(alias, StringComparison.OrdinalIgnoreCase);
                if (index >= 0) return Success(markedString, index, alias.Length, entry.Key);
            }

            return null;
        }

        protected abstract bool Precondition(IEnumerable<DetectedAddress> detects, int iteration);
        protected abstract IDictionary<string, IEnumerable<string>> GetDictionary();
        protected abstract DetectedAddress Success(MarkedString markedString, int index, int length, string key);
    }
}