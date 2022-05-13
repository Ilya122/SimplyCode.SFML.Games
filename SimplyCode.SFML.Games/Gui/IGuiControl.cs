using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.Gui
{
    public interface IGuiControl : Drawable, IUpdatable, IDisposable
    {
        /// <summary>
        /// Returns the top-left most position.
        /// </summary>
        Vector2f Position { get; set; }

        // TODO: Create a base gui control for controls that are built from different controls (Like "UserControl" in Winforms)
        // Reason is to aggregate more easily gui controls for bound checks + Cache results.
        /// <summary>
        /// Returns the size of the gui control on screen in pixels.
        /// </summary>
        FloatRect Bounds { get; }
    }
}