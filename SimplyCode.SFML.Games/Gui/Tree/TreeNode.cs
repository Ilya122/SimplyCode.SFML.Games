using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace SimplyCode.SFML.Games.Gui.Tree
{

    /// <summary>
    /// Visual Tree Node in a TreeControl
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public abstract class TreeNode : IGuiControl
    {
        protected Window mWindowParent;

        //TODO: IButton is a very naive choice - Should be defined by a more generic choice which supports Inputs.
        protected IButton mContent;

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }


        public event Action<TreeNode> OnSelected;

        public Vector2f Position { get => mContent.Position; set => mContent.Position = value; }
        public FloatRect Bounds => mContent.Bounds;

        public TreeNode Parent { get; set; }
        public ICollection<TreeNode> Children { get; private set; }
        public string DisplayedText { get; set; }
        public int Level { get; set; }
        public bool Expanded { get; set; }
        public bool Loaded { get; set; }
        public bool Enabled { get; set; }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mContent, states);
        }

        public virtual void Update(Time timeElapsed)
        {
            mContent.Update(timeElapsed);
        }

        public virtual void Dispose()
        {
            mContent.Dispose();
            mContent = null;
            mWindowParent = null;
            Parent = null;
            //OnSelected ?? This should be deselected
            foreach (var child in Children)
            {
                child.Dispose();
            }
        }

        protected void InvokeOnSelected()
        {
            OnSelected?.Invoke(this);
        }
    }

    public static class TreeNodeExtensions
    {
        public static void ForEachChildrenRecurseUp(this TreeNode child, Action<TreeNode> action)
        {
            if (child.Parent == null)
            {
                return;
            }

            foreach (var n in child.Parent.Children)
            {
                action(n);
                ForEachChildrenRecurseUp(n.Parent, action);
            }
        }

        public static void ForEachRescurse(this TreeNode root, Action<TreeNode> action)
        {
            foreach (var child in root.Children)
            {
                action(child);
                ForEachRescurse(child, action);
            }
        }

        public static float SumRecurse(this TreeNode root, Func<TreeNode, float> action)
        {
            float ret = 0;
            foreach (var child in root.Children)
            {
                ret += action(child);
                ret += SumRecurse(child, action);
            }
            return ret;
        }

        public static TreeNode MinRecurse(this TreeNode root, Func<TreeNode, float> action)
        {
            TreeNode treeMin = null;
            float minA = float.MaxValue;
            foreach (var child in root.Children)
            {
                var min = action(child);
                if (min < minA)
                {
                    treeMin = child;
                    minA = min;
                }
                if (child.Children.Count > 0)
                {
                    treeMin = MinRecurse(child, action);
                }
            }

            return treeMin;
        }
        public static TreeNode MaxRecurse(this TreeNode root, Func<TreeNode, float> action)
        {
            TreeNode treeMin = null;
            float maxA = float.MinValue;
            foreach (var child in root.Children)
            {
                var max = action(child);
                if (max > maxA)
                {
                    treeMin = child;
                    maxA = max;
                }

                if (child.Children.Count > 0)
                {
                    treeMin = MaxRecurse(child, action);
                }
            }

            return treeMin;
        }
    }
}
