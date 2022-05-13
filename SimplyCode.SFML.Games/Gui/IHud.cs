using SFML.Graphics;
using System;

namespace SimplyCode.SFML.Games.Gui
{


    /// <summary>
    /// There's probably a better design, for V0.2 release it's ok.
    /// TODO: Redesign it.
    /// </summary>
    public class HudMessage
    {
        public string ActionName { get; set; }

    }

    public interface IHud : Drawable, IUpdatable, IDisposable
    {
        /// <summary>
        /// A notification that something has happened
        /// </summary>
        event Action<HudMessage> GeneralNotification;
    }
}
