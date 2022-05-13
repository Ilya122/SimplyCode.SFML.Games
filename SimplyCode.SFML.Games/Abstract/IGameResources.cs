using SFML.Audio;
using SFML.Graphics;

namespace SimplyCode.SFML.Games
{
    public enum ShaderType
    {
        Vertex,
        Fragment,
        Geometry
    }

    public interface IGameResources
    {
        Texture GetTexture(string id);

        Sound GetSound(string id);

        Font GetFont(string id);

        Shader GetShader(string id, ShaderType type);

        Shader GetShader(string vertexId, string fragmentId);

        Shader GetShader(string vertexId, string fragmentId, string geometryId);
    }
}