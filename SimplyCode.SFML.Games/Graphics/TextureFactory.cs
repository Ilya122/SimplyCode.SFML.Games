using SFML.Graphics;

namespace SimplyCode.SFML.Games.Graphics
{
    public static class TextureFactory
    {
        public static Texture CreateTexture(uint width, uint height, Color fillColor)
        {
            Color[,] pixels = new Color[width, height];

            for (int x = 0; x < pixels.GetLength(0); x++)
                for (int y = 0; y < pixels.GetLength(1); y++)
                    pixels[x, y] = fillColor;

            var image = new Image(pixels);
            return new Texture(image);
        }

        public static Texture CreateHollowTexture(uint width, uint height, Color lineColor, uint lineWidth)
        {
            Color[,] pixels = new Color[width, height];
            // TOP
            for (uint x = 0; x < width; x++)
                for (uint y = 0; y < lineWidth; y++)
                {
                    pixels[x, y] = lineColor;
                }
            // Bottom
            for (uint x = 0; x < width; x++)
                for (uint y = height - lineWidth; y < height; y++)
                {
                    pixels[x, y] = lineColor;
                }

            // Left
            for (uint y = 0; y < height; y++)
                for (uint x = 0; x < lineWidth; x++)
                {
                    pixels[x, y] = lineColor;
                }
            // Right
            for (uint y = 0; y < height; y++)
                for (uint x = width - lineWidth; x < width; x++)
                {
                    pixels[x, y] = lineColor;
                }

            var image = new Image(pixels);
            return new Texture(image);
        }

    }
}