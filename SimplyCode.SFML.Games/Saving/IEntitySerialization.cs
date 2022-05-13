using System.IO;

namespace SimplyCode.SFML.Games.Saving
{

    public interface IEntitySerialized
    {
        void SerializeInto(Stream stream);
    }   
    public interface IEntityDeserialized
    {
        void DeserializeFrom(Stream stream);
    }


    /// <summary>
    /// Defines an entity which will be serialized into the stream
    /// </summary>
    public interface IEntitySerialization : IEntitySerialized, IEntityDeserialized
    {
    }
}
