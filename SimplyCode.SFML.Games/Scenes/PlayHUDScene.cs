using SimplyCode.SFML.Games.Gui;
using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.Scenes
{
    public class PlayHUDScene : Scene
    {
        protected IHud mHUD;

        public PlayHUDScene(IHud hud)
        {
            mHUD = hud;
        }

        public override void Switched(SceneChangeState state)
        {
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mHUD, states);
        }

        public override void Update(Time timeElapsed)
        {
            mHUD.Update(timeElapsed);
        }

        public override void Dispose()
        {
            mHUD.Dispose();
            mHUD = null;
        }
    }
}
