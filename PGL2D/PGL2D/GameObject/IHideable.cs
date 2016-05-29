namespace PGL2D.GameObject
{
    public interface IHideable
    {
        /// <summary>
        /// Determines if the object is hidden and should not be drawn
        /// </summary>
        bool IsHidden { get; set; }
    }
}
