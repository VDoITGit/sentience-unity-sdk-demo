using System;

namespace Sentience.Utils
{
    public static class BoolExtensions
    {
        public static byte[] ToByteArray(this bool value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}
