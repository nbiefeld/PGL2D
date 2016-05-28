using Microsoft.Xna.Framework;

namespace PGL2D.Utility
{
    public static class Vector2Ex
    {
        public static Vector2 Reflect(this Vector2 v, Vector2 n)
        {
            return v + Vector2.Multiply(n, 2 * Vector2.Dot(-v, n));
        }
    }
}
