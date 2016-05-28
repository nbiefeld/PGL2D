using System;

namespace PGL2D.Utility
{
    public static class RandomEx
    {
        public static int NextOdd(this Random random, int min, int max)
        {
            if (min == max)
            {
                throw new ArgumentException("Max cannot equal Min", nameof(max));
            }

            int num;
            do
            {
                num = random.Next(min, max);
            } while (num % 2 == 0);

            return num;
        }
    }
}
