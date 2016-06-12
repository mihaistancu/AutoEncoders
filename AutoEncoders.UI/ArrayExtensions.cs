using System;
using System.Linq;

namespace AutoEncoders.UI
{
    public static class ArrayExtensions
    {
        public static int MaxIndex(this double[] vector)
        {
            return Array.IndexOf(vector, vector.Max());
        }
    }
}
