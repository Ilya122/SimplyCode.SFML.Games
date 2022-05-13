using SFML.Graphics;

namespace SimplyCode.SFML.Games.Abstract
{
    public interface IRenderedGame
    {
        Color CurrentClearColor { get; }
        void ChangeClearColor(Color color);
    }
}
