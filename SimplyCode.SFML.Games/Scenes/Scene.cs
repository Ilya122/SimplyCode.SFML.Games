using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.Scenes
{
    public abstract class Scene : IScene
    {
        public Scene()
        {
            Id = GetType().Name;
        }

        public bool IsActive { get; set; }

        public string Id { get; private set; }

        public event Action<SceneChangeState> OnSceneChangeRequest;

        public virtual void Load() { }
        public virtual void Unload() { }

        public abstract void Dispose();
        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void Switched(SceneChangeState state);

        public abstract void Update(Time timeElapsed);

        protected void RaiseOnSceneChangeRequest(SceneChangeState state)
        {
            OnSceneChangeRequest?.Invoke(state);
        }
    }
}
