using Microsoft.Xna.Framework;

namespace PGL2D.GameObject
{
    public interface IPhysical
    {
        Color Color { get; }
        float Rotation { get; }
        float Scale { get; }
        Vector2 ScaleVector { get; }
        Vector2 Position { get; }
    }
}
