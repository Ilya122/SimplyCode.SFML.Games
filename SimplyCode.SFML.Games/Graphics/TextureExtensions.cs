using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.Graphics
{
    public static class TextureExtensions
    {
        public static Texture ChangeColor(this Texture original, Color originalColor, Color changeTo)
        {
            var image = original.CopyToImage();
            for (uint x = 0; x < image.Size.X; x++)
                for (uint y = 0; y < image.Size.Y; y++)
                {
                    if (image.GetPixel(x, y) == originalColor)
                    {
                        image.SetPixel(x, y, changeTo);
                    }
                }
            return new Texture(image);
        }

        public static Texture Resize(this Texture original, Vector2u newSize)
        {
            var image = original.CopyToImage();
            return new Texture(image.Resize(newSize));
        }
    }
}
