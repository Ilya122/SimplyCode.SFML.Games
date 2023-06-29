using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SimplyCode.SFML.Games.Gui
{
    //TODO: Split into "IIOGuiControl" - receives IO like mouse and keyboard and "IFocusable" - Able to show focus

    /// <summary>
    /// Implements basic behavior of a mouse button.
    /// TODO: Keyboard + Focus behavior.(In other class not in here)
    /// </summary>
    public abstract class BaseButton : IButton
    {
        protected Window mParent;
        /// <summary>
        /// To avoid multiple rapid presses a delay counter is used.
        /// </summary>
        protected TimeSpan mPressDelay = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// Fixes a bug where you could hold left button to press other buttons
        /// If it's only idle it can be pressed.
        /// The button is idle when the mouse is hovering it and not pressing.
        /// </summary>
        private bool mIsIdle = false;
        private bool mIsPressed;
        private bool mIsHovering;
        private bool mDoneHovering;
        private DateTime mLastPressingTime = DateTime.Now;


        public BaseButton(Window parent)
        {
            mParent = parent;
        }

        public abstract Vector2f Position { get; set; }
        public abstract FloatRect Bounds { get; }
        public bool Enabled { get; set; }

        // TODO: Think of better way to do behavior.
        public IControlBehavior HoverBehavior { get; set; }
        public IControlBehavior ReleaseBehavior { get; set; }

        public event Action OnPress;

        public event Action OnRelease;

        public event Action OnHover;
        public event Action OnStopHovering;

        public virtual void Dispose()
        {

        }

        public abstract void Draw(RenderTarget target, RenderStates states);

        public void Update(Time timeElapsed)
        {
            var localViewPos = Mouse.GetPosition(mParent);
            var isHover = Bounds.Contains(localViewPos.X, localViewPos.Y);
            var isLeftPressed = Mouse.IsButtonPressed(Mouse.Button.Left);

            if (isHover && !isLeftPressed)
            {
                mIsIdle = true;
            }
            else if (!isHover)
            {
                mIsIdle = false;
            }

            if (!mIsHovering && isHover)
            {
                mIsHovering = true;
                mDoneHovering = true;
                DoOnHover();
                OnHover?.Invoke();
            }
            else if (mDoneHovering && !isHover)
            {
                mDoneHovering = false;
                mIsHovering = false;
                OnDoneHovering();
                OnStopHovering?.Invoke();
            }

            if (isHover && mIsIdle && isLeftPressed && !mIsPressed && DateTime.Now >= mLastPressingTime + mPressDelay)
            {
                mIsPressed = true;
                mIsIdle = false;
                mLastPressingTime = DateTime.Now;
                DoOnPress();
                OnPress?.Invoke();
            }
            else if (isHover && isLeftPressed && mIsPressed)
            {
                mIsPressed = false;
            }


            if (mIsPressed && !isLeftPressed)
            {
                mIsPressed = false;
                DoOnRelease();
                OnRelease?.Invoke();
            }
        }


        protected virtual void DoOnHover()
        {
        }

        protected virtual void OnDoneHovering()
        {
        }

        protected virtual void DoOnPress()
        {
        }

        protected virtual void DoOnRelease()
        {
        }


    }
}