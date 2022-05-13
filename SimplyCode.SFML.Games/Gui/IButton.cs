using System;

namespace SimplyCode.SFML.Games.Gui
{
    public interface IButton : IGuiControl
    {
        event Action OnPress;

        event Action OnRelease;

        event Action OnHover;

        IControlBehavior HoverBehavior { get; set; }
        IControlBehavior ReleaseBehavior { get; set; }
    }
}