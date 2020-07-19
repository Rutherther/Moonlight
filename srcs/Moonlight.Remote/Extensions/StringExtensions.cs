using System;
using System.Linq;

namespace Moonlight.Remote.Extensions
{
    public static class StringExtensions
    {
        public static string ToHex(this string input)
            => String.Concat(input.Select(x => ((int)x).ToString("X2")));
    }
}