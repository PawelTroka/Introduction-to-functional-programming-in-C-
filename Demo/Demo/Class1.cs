using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo2
{
    public class ValuesContainer
    {
        private readonly IEnumerable<int> _values=Enumerable.Range(0,10);
        
        public void GetRange(out int min, out int max)
        {
            min = _values.Min();
            max = _values.Max();
        }
    }

}

namespace Demo
{
    public struct Range
    {
        public int Min;
        public int Max;
    }
    public class ValuesContainer
    {
        private readonly IEnumerable<int> _values = Enumerable.Range(0, 10);

        public Range GetRange() =>
            new Range() { Min = _values.Min(), Max = _values.Max() };
    }

}

