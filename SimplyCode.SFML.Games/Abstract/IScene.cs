using SimplyCode.SFML.Games.Scenes;
using SFML.Graphics;
using System;

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

    public interface IScene : IActiveResourceProvider, Drawable, IUpdatable, IDisposable
    {
        string Id { get; }
        bool IsActive { get; set; }

        event Action<SceneChangeState> OnSceneChangeRequest;

        void Switched(SceneChangeState state);
    }
}