using SimplyCode.SFML.Games.Saving;
using SFML.Graphics;
using SFML.System;
using System;
using System.IO;

namespace SimplyCode.SFML.Games.Extensions
{
    public static class SpriteExtensions
    {
        public static void SerializeInto(this Sprite sprite, Stream outputStream)
        {
            BinaryWriter writer = new BinaryWriter(outputStream);

            sprite.Color.SerializeInto(outputStream);

            sprite.Position.SerializeInto(outputStream);
            writer.Write(sprite.Rotation);
            sprite.Scale.SerializeInto(outputStream);
            sprite.Origin.SerializeInto(outputStream);

            //TODO: Need to put TextureRect of Sprite as well

            var texture = sprite.Texture;

            writer.Write(texture.Repeated);
            writer.Write(texture.Smooth);
            writer.Write(texture.Srgb);
            writer.Write(texture.Size.X);
            writer.Write(texture.Size.Y);

            var image = texture.CopyToImage();
            writer.Write(image.Pixels.Length);
            writer.Write(image.Pixels, 0, image.Pixels.Length);

        }

        public static Sprite DeserializeSprite(this Stream inputStream)
        {
            BinaryReader reader = new BinaryReader(inputStream);
            var spriteColor = inputStream.DeserializeColor();

            Vector2f pos = inputStream.DeserializeVector2f();
            float rotation = reader.ReadSingle();
            Vector2f scale = inputStream.DeserializeVector2f();
            Vector2f origin = inputStream.DeserializeVector2f();

            //TODO: Read TextureRect from into sprite

            var repeated = reader.ReadBoolean();
            var smooth = reader.ReadBoolean();
            var Srgb = reader.ReadBoolean();
            var sizeX = reader.ReadUInt32();
            var sizeY = reader.ReadUInt32();


            var sizeOfPixelArray = reader.ReadInt32();

            var textureBytes = reader.ReadBytes(sizeOfPixelArray);

            var tex = new Texture(new Image(sizeX, sizeY, textureBytes))
            {
                Repeated = repeated,
                Smooth = smooth,
                Srgb = Srgb
            };

            var sprite = new Sprite(tex)
            {
                Color = spriteColor,
                Position = pos,
                Rotation = rotation,
                Scale = scale,
                Origin = origin
            };

            return sprite;
        }
    }

    public class SpriteEntitySerializationAdapter : IEntitySerialization
    {
        /// <summary>
        /// The result and input for adapter serialization.
        /// </summary>
        public Sprite Sprite { get; set; }

        public void DeserializeFrom(Stream stream)
        {
            Sprite = stream.DeserializeSprite();
        }

        public void SerializeInto(Stream stream)
        {
            Sprite.SerializeInto(stream);
        }
    }

}
