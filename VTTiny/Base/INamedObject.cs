namespace VTTiny.Base
{
    /// <summary>
    /// Interface shared by all objects that have a name.
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        string Name { get; set; }
    }
}
