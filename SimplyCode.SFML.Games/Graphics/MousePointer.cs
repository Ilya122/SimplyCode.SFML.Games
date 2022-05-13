using SimplyCode.SFML.Games.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Graphics
{
    public class MousePointer : Drawable, IUpdatable
    {
        private readonly RenderWindow mParentWindow;
        private readonly Sprite mSprite;
        private readonly Text mCoordinateText;

        public MousePointer(RenderWindow window, Texture tex)
        {
            mParentWindow = window;
            mSprite = new Sprite(tex);
            mCoordinateText = new Text("(0,0)", new Font("ARIAL.TTF"));
            mCoordinateText.Position = new Vector2f(0, 0);
        }

        public MousePointer(RenderWindow window, FloatRect rect, Color color, uint lineWidth = 3)
        {
            mParentWindow = window;
            mSprite = new Sprite(TextureFactory.CreateHollowTexture((uint)rect.Width, (uint)rect.Height, color, lineWidth));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mSprite, states);
            target.Draw(mCoordinateText, states);
        }

        public void Update(Time timeElapsed)
        {
            var pos = Mouse.GetPosition(mParentWindow);
            var mousePosition = mParentWindow.MapPixelToCoords(pos);
            mSprite.Position = new Vector2f(mousePosition.X, mousePosition.Y);
            mCoordinateText.DisplayedString = $"({pos.X},{pos.Y})";
        }
    }
}
