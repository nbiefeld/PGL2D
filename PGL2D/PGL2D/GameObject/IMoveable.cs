using Microsoft.Xna.Framework;

namespace PGL2D.GameObject
{
    public interface IMoveable
    {
        /// <summary>
        /// The direction of the object's movement
        /// </summary>
        float Angle { get; }

        /// <summary>
        /// The speed of the object's movement
        /// </summary>
        float Speed { get; }

        /// <summary>
        /// The velocity of the object's movement
        /// </summary>
        Vector2 Velocity { get; }

        /// <summary>
        /// The acceleration of the object's movement
        /// </summary>
        Vector2 Acceleration { get; }
    }
}
