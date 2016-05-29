namespace PGL2D.GameObject
{
    public interface IRemoveable
    {
        /// <summary>
        /// Determines if this object should be removed in the next game loop
        /// </summary>
        bool RequestDeletion { get; }
    }
}
