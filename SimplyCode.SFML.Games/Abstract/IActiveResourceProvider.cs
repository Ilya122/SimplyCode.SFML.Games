namespace SimplyCode.SFML.Games
{
    /// <summary>
    /// Behavior of loading and unloading "Active" data.
    /// Active data is defined as data only needed when the entity is active on screen.
    /// </summary>
    public interface IActiveResourceProvider
    {
        void Load();
        void Unload();
    }
}