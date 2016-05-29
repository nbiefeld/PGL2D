namespace PGL2D.GameObject
{
    public interface ICollidable<in T> where T : GameEntity
    {
        /// <summary>
        /// Checks to see if this objects collides with a GameEntity
        /// </summary>
        /// <param name="entity">The GameEntity to check for collision</param>
        /// <returns>True if a collision occurred</returns>
        bool CheckCollision(T entity);
    }
}
