using System;

namespace SimplyCode.SFML.Games.Saving
{
    public interface ISaveArrange
    {
        ISaveArrange Save<T>(T item, string id) where T : IEntitySerialized;

        void Pack();
    }

    public interface ILoadArrange
    {

        ILoadArrange Load<T>(string id) where T : IEntityDeserialized, new();
        ILoadArrange Load<T>(string id, params object[] ctorArguments) where T : IEntityDeserialized;

        IUnpackedParameters Unpack();
    }

    public interface IUnpackedParameters : IDisposable
    {
        T Get<T>(string id);
    }

}
