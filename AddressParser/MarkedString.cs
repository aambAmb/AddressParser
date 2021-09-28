using System.Text;

namespace AddressParser
{
    public class MarkedString
    {
        private readonly bool[] _usedChar;
        private readonly string _value;

        public MarkedString(string s)
        {
            _value = s;
            _usedChar = new bool[s.Length];
        }

        public string GetRestString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _usedChar.Length; i++)
                if (_usedChar[i] == false)
                    sb.Append(_value[i]);
                else
                    sb.Append(' ');

            return sb.ToString();
        }

        public void Mark(int startIndex, int matchLength)
        {
            for (var i = startIndex; i < startIndex + matchLength; i++) _usedChar[i] = true;
        }
    }
}