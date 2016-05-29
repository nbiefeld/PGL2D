using Microsoft.Xna.Framework;

namespace PGL2D.GameObject
{
    public interface IPhysical
    {
        /// <summary>
        /// The color tint of our object
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// The rotation of our object
        /// </summary>
        float Rotation { get; }

        /// <summary>
        /// The scale of our object
        /// </summary>
        float Scale { get; }

        /// <summary>
        /// The X and Y scale of our object
        /// </summary>
        Vector2 ScaleVector { get; }

        /// <summary>
        /// The physical location of our object in the game world
        /// </summary>
        Vector2 Position { get; }
    }
}
