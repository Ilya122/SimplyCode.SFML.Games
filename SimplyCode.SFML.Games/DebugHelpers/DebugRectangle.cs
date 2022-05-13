using SimplyCode.SFML.Games.Graphics;
using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.DebugHelpers
{
    public class DebugRectangle : Drawable
    {
        private readonly Sprite mRectangle;

        public DebugRectangle(FloatRect rect, Color color, uint lineWidth = 3)
        {
            mRectangle = new Sprite(TextureFactory.CreateHollowTexture((uint)Math.Abs(rect.Width), (uint)Math.Abs(rect.Height), color, lineWidth));
        }

        public Vector2f Position
        {
            get { return mRectangle.Position; }
            set { mRectangle.Position = value; }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            mRectangle.Draw(target, states);
        }
    }
}
