using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Extensions
{
    public static class TransformableExtensions
    {
        public static void CenterX(this Transformable transformable, FloatRect globalBounds, Window window)
        {
            var viewCenterX = window.Size.X / 2;
            transformable.Position = new Vector2f(viewCenterX - globalBounds.Width / 2, transformable.Position.Y);
        }

        public static void CenterY(this Transformable transformable, FloatRect globalBounds, Window window)
        {
            var viewCenterY = window.Size.Y / 2;
            transformable.Position = new Vector2f(transformable.Position.X, viewCenterY - globalBounds.Height);
        }
    }
}
