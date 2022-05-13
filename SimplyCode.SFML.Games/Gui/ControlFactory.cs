using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public static class ControlFactory
    {
        public static Text CreateTextCentered(Window parentWindow, IGameResources resources, string text, int axiYMargin = 0)
        {
            var viewCenterX = parentWindow.Size.X / 2;

            Text graphicText = new Text(text, resources.GetFont("Arial.TTF"))
            {
                FillColor = Color.Black,
                CharacterSize = 56
            };

            var singlePTextBounds = graphicText.GetGlobalBounds();
            graphicText.Position = new Vector2f(viewCenterX - singlePTextBounds.Width / 2, axiYMargin + singlePTextBounds.Height / 2);
            return graphicText;
        }


        public static TextButton CreateTextButtonCentered(Window parentWindow, IGameResources resources, string text, int axiYMargin = 0)
        {
            var viewCenterX = parentWindow.Size.X / 2;

            Text graphicText = new Text(text, resources.GetFont("Arial.TTF"))
            {
                FillColor = Color.Black,
                CharacterSize = 56
            };

            var singlePTextBounds = graphicText.GetGlobalBounds();
            graphicText.Position = new Vector2f(viewCenterX - singlePTextBounds.Width / 2, axiYMargin + singlePTextBounds.Height / 2);

            return new TextButton(graphicText, parentWindow);
        }

        public static TextBox CreateTextBoxCentered(Window parentWindow, IGameResources resources, string text, int axiYMargin = 0)
        {
            uint charSize = 56;
            var viewCenterX = parentWindow.Size.X / 2;

            var font = resources.GetFont("Arial.TTF");
            Text graphicText = new Text(text, resources.GetFont("Arial.TTF"))
            {
                FillColor = Color.Yellow,
                CharacterSize = charSize
            };

            var singlePTextBounds = graphicText.GetGlobalBounds();
            var pos = new Vector2f(viewCenterX - singlePTextBounds.Width / 2, axiYMargin + singlePTextBounds.Height / 2);

            Vector2u controlSize = new Vector2u((uint)graphicText.GetLocalBounds().Width, charSize);
            return new TextBox(parentWindow, font, charSize, pos, controlSize);
        }

        public static SpriteButton CreateSpriteButtonCentered(Window parentWindow, IGameResources resources, string textureID, int axiYMargin = 0)
        {
            var viewCenterX = parentWindow.Size.X / 2;

            var texture = resources.GetTexture(textureID);
            var sprite = new SpriteButton(texture, parentWindow);
            var singlePTextBounds = texture.Size;
            sprite.Position = new Vector2f(viewCenterX - singlePTextBounds.X / 2, axiYMargin + singlePTextBounds.Y / 2);

            return sprite;
        }

    }
}