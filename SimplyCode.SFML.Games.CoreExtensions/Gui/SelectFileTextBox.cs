using SimplyCode.SFML.Games.Gui;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SFMLSystem = SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Scene.Extensions
{
    public class SelectFileTextBox : IGuiControl
    {
        private Text mFilePathText;
        private Text mSelectText;
        private SpriteButton mButton;
        private Font mFont;
        private readonly IEnumerable<string> mSupportedImageTypesExtensions;

        public SelectFileTextBox(Window parentWindow, Texture buttonTexture, Font textFont,
            SFMLSystem.Vector2f leftMostPosition, IEnumerable<string> supportedImageForamts)
        {
            mSupportedImageTypesExtensions = supportedImageForamts;

            Position = leftMostPosition;
            mButton = new SpriteButton(buttonTexture, parentWindow)
            {
                Position = leftMostPosition
            };

            const int margin = 10;

            mFilePathText = new Text(string.Empty, textFont)
            {
                Position = new SFMLSystem.Vector2f(leftMostPosition.X + buttonTexture.Size.X + margin, leftMostPosition.Y),
                CharacterSize = 32,
                FillColor = Color.Black
            };


            mSelectText = new Text(string.Empty, textFont)
            {
                Position = new SFMLSystem.Vector2f(leftMostPosition.X + buttonTexture.Size.X + margin, leftMostPosition.Y),
                CharacterSize = 32,
                FillColor = Color.Black,
                DisplayedString = "Select an image file"
            };
            mButton.OnPress += SelectFilePressed;
            mFont = textFont;
        }

        public bool UseWindowsFileSelection { get; set; }
        private void SelectFilePressed()
        {
            if (UseWindowsFileSelection)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                StringBuilder filter = new StringBuilder();
                filter.Append("Images|");
                foreach (var format in mSupportedImageTypesExtensions)
                {
                    filter.Append("*.");
                    filter.Append(format);
                    filter.Append(";");
                }
                fileDialog.Filter = filter.ToString();

                var dialogResult = fileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    mFilePathText.DisplayedString = fileDialog.FileName;
                }
            }
            else
            {
                using SelectFileWindow dialog = new SelectFileWindow(mFont);
                dialog.Open();
                mFilePathText.DisplayedString = dialog.SelectedFile;
            }
        }

        public SFMLSystem.Vector2f Position { get; set; }

        public string SelectedFile { get => mFilePathText.DisplayedString; set => mFilePathText.DisplayedString = value; }

        public FloatRect Bounds => throw new NotImplementedException();

        public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (string.IsNullOrEmpty(mFilePathText.DisplayedString))
            {
                mSelectText.Draw(target, states);
            }
            mFilePathText.Draw(target, states);
            mButton.Draw(target, states);
        }

        public void Update(SFMLSystem.Time timeElapsed)
        {
            mButton.Update(timeElapsed);
        }

        public void Dispose()
        {
            mButton.Dispose();
            mButton = null;
            mFilePathText.Dispose();
            mFilePathText = null;
            mSelectText.Dispose();
            mSelectText = null;
        }
    }
}