using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.Gui
{
    /// <summary>
    /// Base class for Gui windows.
    /// </summary>
    public interface IGuiWindow : IUpdatable, Drawable, IDisposable
    {
        bool IsOpen { get; }
        Vector2i Position { get; set; }

        void Close();
        void Open();
    }
}