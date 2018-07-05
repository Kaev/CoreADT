using System;
using System.Globalization;

namespace CoreADT.Helper
{
    public static class BitHelper
    {
        public static bool GetBit<T>(this T input, int position) where T : struct, IConvertible
        {
            var value = input.ToInt64(CultureInfo.CurrentCulture);
            return (value & (1 << position)) != 0;
        }

        public static void SetBit(this long input, int position, bool value)
        {
            if (value)
                input |= 1 << position;
            else
                input &= ~(1 << position);
        }
    }
}
