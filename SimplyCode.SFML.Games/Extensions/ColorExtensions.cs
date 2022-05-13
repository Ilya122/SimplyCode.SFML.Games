using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimplyCode.SFML.Games.Extensions
{
    public static class ColorExtensions
    {
        //Color
        public static void SerializeInto(this Color color, Stream outputStream)
        {
            BinaryWriter writer = new BinaryWriter(outputStream);
            //TODO: Extension serialization for Color.
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
            writer.Write(color.A);
        }

        public static Color DeserializeColor(this Stream inputStream)
        {
            BinaryReader reader = new BinaryReader(inputStream);
            var spriteColorR = reader.ReadByte();
            var spriteColorG = reader.ReadByte();
            var spriteColorB = reader.ReadByte();
            var spriteColorA = reader.ReadByte();
            return new Color
            {
                R = spriteColorR,
                G = spriteColorG,
                B = spriteColorB, 
                A = spriteColorA
            };
        }
    }
}
