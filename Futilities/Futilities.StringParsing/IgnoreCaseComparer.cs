using System;
using System.Collections.Generic;
using System.Linq;

namespace Futilities.StringParsing
{
    [Obsolete("Please use the built-in case-insensitive comparer: System.StringComparer.OrdinalIgnoreCase", false)]
    public class IgnoreCaseComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => string.Compare(x, y, true) == 0;

        public int GetHashCode(string obj) => obj.GetHashCode();
    }
}
