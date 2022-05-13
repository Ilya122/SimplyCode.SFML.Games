using SimplyCode.SFML.Games.Gui.Tree;
using SFML.Graphics;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui.SelectFile
{
    public class FileTreeNode : TreeNode
    {
        /// <summary>
        /// It's a safer way to handle "Regirsation" like event based.  
        /// Especially with a cyclic behavior like a tree view.
        /// </summary>
        protected ITreeNodeSelection mSelectionHandler;

        public FileTreeNode(Window parent, string filePath, Font font, Color fontColor, ITreeNodeSelection selectionHandler)
        {
            mContent = new TextButton(new Text(filePath, font) { FillColor = fontColor, CharacterSize = 15 }, parent);
            mContent.OnPress += DoOnPress;
            DisplayedText = filePath;
            mSelectionHandler = selectionHandler;
        }
        public override void Dispose()
        {
            mContent.OnPress -= DoOnPress;
            base.Dispose();
        }

        private void DoOnPress()
        {
            mSelectionHandler.OnSelected(this);
        }
    }
}
