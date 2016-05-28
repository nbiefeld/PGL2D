namespace PGL2D.GameObject
{
    public interface ICollidable<in T> where T : GameEntity
    {
        bool CheckCollision(T entity);
    }
}
