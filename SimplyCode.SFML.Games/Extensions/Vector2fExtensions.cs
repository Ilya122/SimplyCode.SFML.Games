using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimplyCode.SFML.Games.Extensions
{
    public static class Vector2fExtensions
    {
        public static void SerializeInto(this Vector2f sprite, Stream outputStream)
        {
            BinaryWriter writer = new BinaryWriter(outputStream);

            writer.Write(sprite.X);
            writer.Write(sprite.Y);
        }

        public static Vector2f DeserializeVector2f(this Stream inputStream)
        {
            BinaryReader reader = new BinaryReader(inputStream);

            return new Vector2f
            {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle()
            };
        }
    }
}
