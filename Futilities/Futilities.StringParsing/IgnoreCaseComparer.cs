using System;
using System.Collections.Generic;
using System.Linq;

namespace Futilities.StringParsing
{
    public class IgnoreCaseComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => string.Compare(x, y, true) == 0;

        public int GetHashCode(string obj) => obj.GetHashCode();
    }
}
