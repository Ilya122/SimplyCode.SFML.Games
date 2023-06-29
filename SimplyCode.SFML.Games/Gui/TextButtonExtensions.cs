using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.Gui
{
    public static class TextButtonExtensions
    {
        public static void CenterX(this Text text, RenderWindow window)
        {
            var bounds = text.GetGlobalBounds();
            var windowWidth = window.Size.X;

            var centerX = windowWidth / 2 - bounds.Width / 2;
            text.Position = new Vector2f(centerX, text.Position.Y);
        }

        public static void CenterX(this TextButton button, RenderWindow window)
        {
            var bounds = button.Text.GetGlobalBounds();
            var windowWidth = window.Size.X;

            var centerX = windowWidth / 2 - bounds.Width / 2;
            button.Position = new Vector2f(centerX, button.Position.Y);
        }
    }
}
