using System;

namespace SimplyCode.SFML.Games.Scenes
{
    public class SceneChangeState
    {
        public SceneChangeState(Type requestedScene, Type requesterScene)
        {
            RequestedScene = requestedScene;
            RequesterScene = requesterScene;
            RequestedSceneId = requestedScene.Name;
            RequesterSceneId = requesterScene.Name;
        }

        public SceneChangeState(string requestedSceneId = "", string requesterSceneId = "")
        {
            RequestedSceneId = requestedSceneId;
            RequesterSceneId = requesterSceneId;
        }

        public string RequestedSceneId { get; set; }

        public string RequesterSceneId { get; set; }

        public Type RequestedScene { get; set; }

        public Type RequesterScene { get; set; }
    }

    public class SceneChangeState<TRequested, TRequester> : SceneChangeState
    {
        public SceneChangeState()
            : base(typeof(TRequested), typeof(TRequester))
        {
            RequestedScene = typeof(TRequested);
            RequesterScene = typeof(TRequester);
        }

        public Type RequestedScene { get; set; }
        public Type RequesterScene { get; set; }
    }
}