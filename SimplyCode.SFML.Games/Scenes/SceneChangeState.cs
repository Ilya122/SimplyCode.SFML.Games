namespace SimplyCode.SFML.Games.Scenes
{
    public class SceneChangeState
    {
        public SceneChangeState(string requestedSceneId = "", string requesterSceneId = "")
        {
            RequestedSceneId = requestedSceneId;
            RequesterSceneId = requesterSceneId;
        }

        public string RequestedSceneId { get; set; }

        public string RequesterSceneId { get; set; }
    }

    public class SceneChangeState<TRequested, TRequester> : SceneChangeState
    {
        public SceneChangeState()
            : base(typeof(TRequested).Name, typeof(TRequester).Name) { }
    }
}