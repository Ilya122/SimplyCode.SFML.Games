namespace SimplyCode.SFML.Games.Gui.SelectFile
{
    public interface ITreeNodeSelection
    {
        /// <summary>
        /// Only FileTreeNodes can be selected.
        /// </summary>
        /// <param name="node"></param>
        void OnSelected(FileTreeNode node);
    }
}
