using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimplyCode.SFML.Games.Saving.DefaultSerializers
{
    public class StringEntitySerialization : IEntitySerialization
    {
        public string Data { get; set; }

        public void DeserializeFrom(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            Data = reader.ReadString();
        }

        public void SerializeInto(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Data);
        }
    }
}
