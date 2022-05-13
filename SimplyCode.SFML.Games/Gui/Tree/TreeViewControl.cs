using SimplyCode.SFML.Games.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Linq;

namespace SimplyCode.SFML.Games.Gui.Tree
{
    public abstract class TreeViewControl : IGuiControl
    {
        protected Sprite mTreeBoundingBox;

        //TODO: To "ScrollView" class
        protected Vector2f mStartPosition;
        protected Sprite mBoundingScrollBox;
        protected Sprite mScrollBox;
        protected bool mIsDragging = false;
        protected Vector2i mPreviousMousePos;

        protected Window mParentWindow;
        protected Font mFont;
        protected const int mJumpInYPosition = 20;

        public TreeViewControl(Window parent, Font font, Vector2f startPosition, Vector2u size)
        {
            mParentWindow = parent;
            mFont = font;

            uint width = size.X;
            uint height = size.Y;

            var scrollTex = TextureFactory.CreateTexture(10, height / 20, Color.Black);
            var boundTex = TextureFactory.CreateHollowTexture(10, height, Color.Black, 2);
            var wholeTreeBoundTex = TextureFactory.CreateHollowTexture(width, height, new Color(40, 40, 40), 2);

            mScrollBox = new Sprite(scrollTex)
            {
                Position = startPosition + new Vector2f(width - 11, 0)
            };
            mBoundingScrollBox = new Sprite(boundTex)
            {
                Position = startPosition + new Vector2f(width - 11, 0)
            };
            mTreeBoundingBox = new Sprite(wholeTreeBoundTex)
            {
                Position = startPosition
            };
            mStartPosition = startPosition;

            mParentWindow.MouseWheelScrolled += MouseWheelScrolled;
        }

        public TreeNode Root { get; set; }

        public virtual Vector2f Position { get => Root.Position; set => Root.Position = value; }

        public FloatRect Bounds { get => mTreeBoundingBox.GetGlobalBounds(); }


        public virtual void Dispose()
        {
            Root.Dispose();
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            Root.ForEachRescurse(n =>
            {
                if (n.Parent.Expanded && n.Position.Y > mTreeBoundingBox.Position.Y &&
                     n.Position.Y < mTreeBoundingBox.Position.Y + mTreeBoundingBox.GetGlobalBounds().Height &&
                     n.Position.Y + n.Bounds.Height < mTreeBoundingBox.Position.Y + mTreeBoundingBox.GetGlobalBounds().Height)
                {
                    target.Draw(n, states);
                }
            });


            if (Root.Position.Y > mTreeBoundingBox.Position.Y &&
                Root.Position.Y < mTreeBoundingBox.Position.Y + mTreeBoundingBox.GetGlobalBounds().Height &&
                Root.Position.Y + Root.Bounds.Height < mTreeBoundingBox.Position.Y + mTreeBoundingBox.GetGlobalBounds().Height)
            {
                target.Draw(Root, states);
            }

            target.Draw(mScrollBox, states);
            target.Draw(mBoundingScrollBox, states);
            target.Draw(mTreeBoundingBox, states);
        }

        public virtual void Update(Time timeElapsed)
        {
            var pos = Mouse.GetPosition(mParentWindow);
            if (Mouse.IsButtonPressed(Mouse.Button.Left) && mScrollBox.GetGlobalBounds().Contains(pos.X, pos.Y))
            {
                // Scrolling
                mIsDragging = true;
            }
            else
            {
                mIsDragging = mIsDragging && Mouse.IsButtonPressed(Mouse.Button.Left);
            }

            Root.Update(timeElapsed);

            if (mIsDragging)
            {
                var diff = pos - mPreviousMousePos;
                Scroll(diff.Y);
            }

            mPreviousMousePos = pos;
        }

        private void MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            if (mTreeBoundingBox.GetGlobalBounds().Contains(e.X, e.Y))
            {
                var delta = e.Delta * 5 * -1;
                Scroll(delta);
            }
        }

        private void Scroll(float delta)
        {
            if (mScrollBox.Position.Y >= mStartPosition.Y
                && mScrollBox.Position.Y <= mStartPosition.Y + mBoundingScrollBox.GetGlobalBounds().Height - mScrollBox.GetGlobalBounds().Height)
            {
                var maxY = mStartPosition.Y + mBoundingScrollBox.GetGlobalBounds().Height - mScrollBox.GetGlobalBounds().Height;
                if (ShouldScroll(delta, mScrollBox.Position, mStartPosition.Y, maxY))
                {
                    return;
                }

                mScrollBox.Position = new Vector2f(mScrollBox.Position.X, mScrollBox.Position.Y + delta);

                float height = Root.SumRecurse(n => n.Bounds.Height);
                //  var allNodeHeights = height + mJumpInYPosition * Root.SumRecurse(t => 1) + mStartPosition.Y;
                var allNodeHeights = mJumpInYPosition * Root.SumRecurse(t => 1) + mStartPosition.Y;

                var scrollBoxHeight = mScrollBox.GetGlobalBounds().Height;
                var scrollHeight = mBoundingScrollBox.GetGlobalBounds().Height;

                var percentageDelta = delta * 100 / scrollHeight;

                var deltaForEachNode = (int)(allNodeHeights * percentageDelta / 100); // We want to scroll all the nodes so we reach the end

                Root.Position = new Vector2f(Root.Position.X, Root.Position.Y - deltaForEachNode);

                Root.ForEachRescurse(node =>
                {
                    node.Position = new Vector2f(node.Position.X, node.Position.Y - deltaForEachNode);
                });
            }
        }

        private bool ShouldScroll(float delta, Vector2f currPos, float minY, float maxY)
        {
            var expectedLocation = currPos.Y + delta;
            return ((delta < 0 && expectedLocation < minY) || (delta > 0 && expectedLocation > maxY));
        }
    }
}
