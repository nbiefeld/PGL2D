using Microsoft.Xna.Framework;

namespace PGL2D.GameObject
{
    public interface IMoveable
    {
        float Angle { get; }
        float Speed { get; }
        Vector2 Velocity { get; }
        Vector2 Acceleration { get; }
    }
}
