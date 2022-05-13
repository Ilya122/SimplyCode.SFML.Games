using SFML.Graphics;
using System;

namespace SimplyCode.SFML.Games
{
    public static class CircleShapeExtensions
    {
        public static bool Intersects(this CircleShape a, CircleShape b)
        {
            var a_square = Math.Pow(a.Position.X - b.Position.X, 2);
            var b_square = Math.Pow(a.Position.Y - b.Position.Y, 2);

            return Math.Sqrt(a_square + b_square) < a.Radius + b.Radius;
        }
    }
}
