using System;
using System.Collections.Generic;
using System.Linq;

namespace YouSchedule.Predictor.Models
{
    public class IntegerGroup : List<int>
    {
        public IntegerGroup(IEnumerable<int> original) : base(original) { }

        public IntegerGroup() : base() { }

        public IntegerGroup CreateCopy()
        {
            return new IntegerGroup(this);
        }

        public int Sum()
        {
            int total = 0;
            for (int i = 0; i < Count; i++)
            {
                total += this[i];
            }
            return total;
        }

        public double Average()
        {
            int total = Sum();
            return (double)total / Count;
        }

        public int Min()
        {
            int min = this[0];
            for (int i = 1; i < Count; i++)
            {
                int v = this[i];
                if (v < min)
                {
                    min = v;
                }
            }
            return min;
        }

        public int Max()
        {
            int max = this[0];
            for (int i = 1; i < Count; i++)
            {
                int v = this[i];
                if (v > max)
                {
                    max = v;
                }
            }
            return max;
        }

        public int Range()
        {
            int min = Min();
            int max = Max();
            return max - min;
        }
    }
}
