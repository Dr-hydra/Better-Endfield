using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStudio
{
    public struct Double
    {
        public static double Frexp(double value, out int exponent)
        {
            if (value == 0.0)
            {
                exponent = 0;
                return 0.0;
            }

            long bits = BitConverter.DoubleToInt64Bits(value);
            long exp = (bits >> 52) & 0x7FFL;

            if (exp == 0)
            {
                value *= System.Math.Pow(2, 54);
                bits = BitConverter.DoubleToInt64Bits(value);
                exp = ((bits >> 52) & 0x7FFL) - 54;
            }

            exponent = (int)(exp - 1022);
            bits = (long)(((ulong)bits & 0x800FFFFFFFFFFFFFL) | 0x3FE0000000000000L);

            return BitConverter.Int64BitsToDouble(bits);
        }
    }
}
