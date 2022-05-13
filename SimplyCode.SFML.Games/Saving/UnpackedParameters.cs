using System.Collections.Generic;

namespace SimplyCode.SFML.Games.Saving
{
    public class UnpackedParameters : IUnpackedParameters
    {
        private IDictionary<string, IEntityDeserialized> mSerialization;
        public UnpackedParameters(IDictionary<string, IEntityDeserialized> serialization)
        {
            mSerialization = serialization;
        }

        public void Dispose()
        {
            mSerialization.Clear();
            mSerialization = null;
        }

        public T Get<T>(string id)
        {
            mSerialization.TryGetValue(id, out var item);
            return (T)item;
        }
    }
}
