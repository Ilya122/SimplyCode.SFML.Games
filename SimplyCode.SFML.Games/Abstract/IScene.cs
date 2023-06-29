using SimplyCode.SFML.Games.Scenes;
using SFML.Graphics;
using System;

namespace SimplyCode.SFML.Games
{ 
    public interface IScene : IActiveResourceProvider, Drawable, IUpdatable, IDisposable
    {
        string Id { get; }
        bool IsActive { get; set; }

        event Action<SceneChangeState> OnSceneChangeRequest;

        void Switched(SceneChangeState state);
    }
}