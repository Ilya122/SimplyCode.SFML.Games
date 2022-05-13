using SimplyCode.SFML.Games.Gui.SelectFile;
using SimplyCode.SFML.Games.Gui.Tree;
using SFML.Graphics;
using SFML.System;
using System.IO;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public class DirectoryTreeNode : TreeNode
    {
        private Font mFont;

        /// <summary>
        /// TODO: Test supported image formats
        /// </summary>
        private string mFileFilter;
        /// <summary>
        /// TODO: To config - Make sure different UI sizes will be reactive.
        /// </summary>
        protected const int mJumpInYPosition = 20;

        /// <summary>
        /// It's a safer way to handle "Regirsation" like event based.  
        /// Especially with a cyclic behavior like a tree view.
        /// </summary>
        protected ITreeNodeSelection mSelectionHandler;

        //TODO: Control character size better through scale and size in GuiControl.
        public DirectoryTreeNode(Window parent, string filePath, Font font, Color fontColor,
            ITreeNodeSelection selectionHandler, string fileFilter = "*")
        {
            mWindowParent = parent;
            mFont = font;
            mSelectionHandler = selectionHandler;
            mContent = new TextButton(new Text(filePath, font) { FillColor = fontColor, CharacterSize = 15 }, parent);
            mFileFilter = fileFilter;
            mContent.OnPress += DoOnPress;
            DisplayedText = filePath;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mContent, states);

            if (Expanded)
            {
                foreach (var child in Children)
                {
                    target.Draw(child, states);
                }
            }
        }
        public override void Update(Time timeElapsed)
        {
            base.Update(timeElapsed);
            if (Expanded)
            {
                foreach (var child in Children)
                {
                    child.Update(timeElapsed);
                }
            }
        }
        private void DoOnPress()
        {
            Expanded = !Expanded;

            if (!Loaded)
            {
                Fill(this, DisplayedText, Level, Level + 1, mFont, Position, "*");
                Loaded = true;
            }

            // Smaller Y means higher on screen!
            var highetYChild = TreeNodeExtensions.MinRecurse(this, c => c.Position.Y);
            var lowestYChild = TreeNodeExtensions.MaxRecurse(this, c => c.Position.Y);
            if (highetYChild == null || lowestYChild == null)
            {
                return;
            }

            var diff = lowestYChild.Position.Y - highetYChild.Position.Y + mJumpInYPosition;
            if (Parent == null)
            {
                return;
            }

            if (Expanded)
            {
                TreeNodeExtensions.ForEachChildrenRecurseUp(this, (n) =>
                {
                    if (n.Position.Y > Position.Y)
                    {
                        n.Position += new Vector2f(0, diff);

                        n.ForEachRescurse(nChildNode =>
                        {
                            nChildNode.Position += new Vector2f(0, diff);
                        });
                    }
                });
            }
            else
            {
                // Smaller Y means higher on screen!
                TreeNodeExtensions.ForEachChildrenRecurseUp(this, (n) =>
                {
                    if (n.Position.Y > Position.Y)
                    {
                        n.Position -= new Vector2f(0, diff);
                        n.Expanded = false;
                        n.ForEachRescurse(nChildNode =>
                        {
                            nChildNode.Position -= new Vector2f(0, diff);
                            nChildNode.Expanded = false;
                        });
                    }
                });
            }
        }

        private Vector2f Fill(TreeNode parentTreeNode, string path, int previousLevel, int maxLevel, Font font, Vector2f startPosition, string fileFormat)
        {
            if (previousLevel == maxLevel)
            {
                return startPosition;
            }

            var increment = 1;
            var newLevel = previousLevel + 1;
            string[] files;

            try
            {
                files = Directory.GetFiles(path, fileFormat);
            }
            catch
            {
                return startPosition;
            }

            Vector2f lastPos = startPosition;

            foreach (var file in files)
            {
                var fileTreeNode = new FileTreeNode(mWindowParent, file, font, Color.Black, mSelectionHandler)
                {
                    Parent = parentTreeNode,
                    Position = new Vector2f(startPosition.X + (10 * newLevel), lastPos.Y + mJumpInYPosition),
                    Level = newLevel
                };

                parentTreeNode.Children.Add(fileTreeNode);
                ++increment;
                lastPos = fileTreeNode.Position;
            }

            var dirIncerements = 1;
            var dirs = Directory.GetDirectories(path);

            foreach (var dir in dirs)
            {
                var dirTreeNode = new DirectoryTreeNode(mWindowParent, dir, font, Color.Black, mSelectionHandler, fileFormat)
                {
                    Parent = parentTreeNode,
                    Position = new Vector2f(startPosition.X + 10 * newLevel, lastPos.Y + mJumpInYPosition * dirIncerements),
                    Level = newLevel
                };

                parentTreeNode.Children.Add(dirTreeNode);
                ++dirIncerements;
                lastPos = Fill(dirTreeNode, dir, newLevel, maxLevel, font, lastPos, fileFormat);
            }

            return lastPos;
        }
    }
}
