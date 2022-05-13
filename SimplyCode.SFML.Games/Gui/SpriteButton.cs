using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public class SpriteButton : BaseButton
    {
        protected Sprite mPicture;

        public SpriteButton(Texture texture, Window parent) : base(parent)
        {
            mPicture = new Sprite(texture);
        }


        public override Vector2f Position { get => mPicture.Position; set => mPicture.Position = value; }

        public override FloatRect Bounds { get => mPicture.GetGlobalBounds(); }

        public Sprite Picture { get => mPicture; }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mPicture, states);
        }
    }
}