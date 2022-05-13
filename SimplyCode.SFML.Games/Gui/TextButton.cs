using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public class TextButton : BaseButton
    {
        private Color mPreviousColor;
        private bool mShouldDisableHoverColor = false;

        public TextButton(Text text, Window parentWindow) : base(parentWindow)
        {
            Text = text;
            HoverColor = Color.Red;
        }
        public TextButton(Text text, Window parentWindow, bool disableHover) : base(parentWindow)
        {
            Text = text;
            mShouldDisableHoverColor = disableHover;
        }

        public Text Text { get; set; }
        public Color HoverColor { get; set; }
        public override Vector2f Position { get => Text.Position; set => Text.Position = value; }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Text, states);
        }

        public override FloatRect Bounds => Text.GetGlobalBounds();

        protected override void DoOnHover()
        {
            if (mShouldDisableHoverColor)
            {
                return;
            }
            mPreviousColor = Text.FillColor;
            Text.FillColor = HoverColor;
        }

        protected override void OnDoneHovering()
        {
            if (mShouldDisableHoverColor)
            {
                return;
            }
            Text.FillColor = mPreviousColor;
        }
    }
}