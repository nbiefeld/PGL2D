namespace PGL2D.GameObject
{
    public interface IFreezable
    {
        /// <summary>
        /// Determines if the object is frozen and should not be updated
        /// </summary>
        bool IsFrozen { get; set; }
    }
}
