using System;

namespace PGL2D.Utility
{
    public static class RandomEx
    {
        /// <summary>
        /// Randomly provides an odd number
        /// </summary>
        /// <param name="random">The Random object to use</param>
        /// <param name="min">The minimum number this can provide</param>
        /// <param name="max">The maximum number this can provide</param>
        /// <exception cref="ArgumentException">Throws an argument exception if min and max are the same</exception>
        /// <returns>A random number between min and max</returns>
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
