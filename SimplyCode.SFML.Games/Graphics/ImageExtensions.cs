using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.Graphics
{
    public static class ImageExtensions
    {
        public static Image Resize(this Image original, Vector2u newSize)
        {
            Vector2u originalImageSize = original.Size;
            var resizedImageSize = newSize;
            Image newImage = new Image(newSize.X, newSize.Y);
            for (uint y = 0; y < resizedImageSize.Y; y++)
            {
                for (uint x = 0; x < resizedImageSize.X; x++)
                {
                    float origX = ((x / (float)resizedImageSize.X) * originalImageSize.X);
                    float origY = ((y / (float)resizedImageSize.Y) * originalImageSize.Y);
                    var pixel = original.GetPixel((uint)origX, (uint)origY);
                    newImage.SetPixel(x, y, pixel);
                }
            }
            return newImage;
        }
    }
}
