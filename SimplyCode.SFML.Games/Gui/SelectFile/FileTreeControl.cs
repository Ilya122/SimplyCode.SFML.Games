using SimplyCode.SFML.Games.Gui.SelectFile;
using SimplyCode.SFML.Games.Gui.Tree;
using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Linq;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public class FileTreeControl : TreeViewControl, ITreeNodeSelection
    {
        private string mLastFilePath; //TOGO into here.

        public FileTreeControl(Window parent, Font font, Vector2f startPosition, Vector2u size, string fileFormat = "*")
            : base(parent, font, startPosition, size)
        {
            StartFill(parent, font, startPosition + new Vector2f(5, 5), fileFormat);
        }

        public event Action<string> OnFileSelected;


        private void StartFill(Window parent, Font font, Vector2f startPosition, string fileFormat)
        {
            var allDrives = DriveInfo.GetDrives();

            static bool isValid(DriveType d)
            {
                return d == DriveType.Fixed || d == DriveType.Ram || d == DriveType.CDRom || d == DriveType.Network;
            }


            var increment = 1;
            Root = new DirectoryTreeNode(parent, "My PC", font, Color.Black, this, "*")
            {
                Position = startPosition
            };

            Vector2f lastPos = startPosition;
            foreach (var drive in allDrives.Where(d => isValid(d.DriveType)))
            {
                var name = drive.Name;
                var dirTreeNode = new DirectoryTreeNode(parent, name, font, Color.Black, this, fileFormat)
                {
                    Parent = Root,
                    Position = lastPos + new Vector2f(0, mJumpInYPosition * increment),
                    Level = 0
                };
                Root.Children.Add(dirTreeNode);
                ++increment;
            }
        }

        public override void Dispose()
        {
            Root.Dispose();
        }

        public void OnSelected(FileTreeNode node)
        {
            OnFileSelected?.Invoke(node.DisplayedText);
        }
    }
}
