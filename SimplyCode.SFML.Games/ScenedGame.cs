using SimplyCode.SFML.Games.Scenes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyCode.SFML.Games
{
    //TODO: DI container when switching to a scene which isn't instanced.

    public abstract class ScenedGame : WindowGame
    {
        protected readonly IList<IScene> mGameScenes = new List<IScene>();
        protected IScene mCurrentScene;

        public ScenedGame(RenderWindow existingWindow, IGameResources resources)
            : base(existingWindow, resources) { }

        protected ScenedGame AddScene(IScene scene)
        {
            scene.OnSceneChangeRequest += SwitchScene;
            mGameScenes.Add(scene);
            return this;
        }
        protected ScenedGame AddCurrentScene(IScene scene)
        {
            scene.IsActive = true;
            mCurrentScene = scene;
            return AddScene(scene);
        }

        protected override void Draw(RenderWindow window)
        {
            mWindow.Draw(mCurrentScene);
        }

        protected override void Update(Time elapsed)
        {
            mCurrentScene.Update(elapsed);
        }

        private void SwitchScene(SceneChangeState state)
        {

            var nextSceneId = state.RequestedSceneId;

            var nextScene = mGameScenes.FirstOrDefault(g => g.Id == nextSceneId);

            mCurrentScene.IsActive = false;

            mCurrentScene = nextScene ?? throw new Exception($"Initialization error - Scene {nextSceneId} was not found.");

            nextScene.IsActive = true;
            nextScene.Switched(state);

        }
    }
}
