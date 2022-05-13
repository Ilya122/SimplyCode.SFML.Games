using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.DebugHelpers
{
    public class FPSCounter : IUpdatable, Drawable, IDisposable
    {
        private uint mFrame = 0;
        private readonly Clock mClock = new Clock();

        private Text mFpsText;

        public FPSCounter(Font fontToUse)
        {
            mFpsText = new Text(string.Empty, fontToUse)
            {
                FillColor = Color.Green,
                Position = new Vector2f(0, 0)
            };
        }

        public uint FPS { get; set; }

        public void Update(Time timeElapsed)
        {
            if (mClock.ElapsedTime.AsSeconds() >= 1.0f)
            {
                FPS = mFrame;
                mFrame = 0;
                mClock.Restart();
            }
            ++mFrame;
            mFpsText.DisplayedString = FPS.ToString();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mFpsText, states);
        }

        public void Dispose()
        {
            mFpsText.Dispose();
            mFpsText = null;
        }
    }
}
