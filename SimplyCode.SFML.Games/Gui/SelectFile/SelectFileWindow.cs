using SimplyCode.SFML.Games.Resources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public class SelectFileWindow : IGuiWindow
    {
        private RenderWindow mWindow;
        private FileTreeControl mTreeControl;
        private TextBox mFilePathTextBox;
        private SpriteButton mCloseButton;
        private SpriteButton mEnterButton;

        private Font mFont;

        private readonly string mFileFormats;
        public SelectFileWindow(Font font, string fileFormats = ".*")
        {
            mFileFormats = fileFormats;
            mFont = font;
        }

        public Vector2i Position
        {
            get { return mWindow.Position; }
            set { mWindow.Position = value; }
        }

        public bool IsOpen => mWindow.IsOpen;
        public string SelectedFile { get; set; }

        public void Open()
        {
            var title = "Select File";
            VideoMode mode = new VideoMode(650, 400);
            Styles styles = Styles.Resize;

            mWindow = new RenderWindow(mode, title, styles);
            mTreeControl = new FileTreeControl(mWindow, mFont, new Vector2f(15, 15), new Vector2u(550, 335));
            mTreeControl.OnFileSelected += FileSelected;

            var closeButtTex = EmbeddedResourcesUtilities.ReadTexture("closeButton.png");
            mCloseButton = new SpriteButton(closeButtTex, mWindow)
            {
                Position = new Vector2f(mode.Width - closeButtTex.Size.X - 5, 5)
            };
            mCloseButton.OnPress += OnCloseWindow;


            Vector2u controlSize = new Vector2u(550, 20);
            mFilePathTextBox = new TextBox(mWindow, mFont, 20, new Vector2f(15, mWindow.Size.Y - 50), controlSize, @"File Path")
            {
                IsReadOnly = true
            };

            var enterButtTex = EmbeddedResourcesUtilities.ReadTexture("enterButton.png");
            mEnterButton = new SpriteButton(enterButtTex, mWindow)
            {
                Position = new Vector2f(mFilePathTextBox.Position.X + mTreeControl.Bounds.Width + 5, mFilePathTextBox.Position.Y)
            };
            mEnterButton.OnPress += OnCloseWindow;

            Clock clock = new Clock();
            var elapsed = clock.Restart();

            while (mWindow.IsOpen)
            {
                mWindow.Clear(Color.White);
                Update(elapsed);
                Draw(mWindow, RenderStates.Default);

                mWindow.DispatchEvents();
                mWindow.Display();
                elapsed = clock.Restart();
            }
        }

        private void OnCloseWindow()
        {
            mWindow.Close();
        }

        private void FileSelected(string obj)
        {
            mFilePathTextBox.Text = obj;
            SelectedFile = obj;
        }

        public void Close()
        {
            if (mWindow != null)
            {
                mWindow.Close();
            }
        }

        public void Dispose()
        {
            mEnterButton.OnPress -= OnCloseWindow;
            mCloseButton.OnPress -= OnCloseWindow;

            mWindow.Dispose();
            mWindow = null;
            mFont = null;
            mTreeControl.Dispose();
            mTreeControl = null;
        }

        public void Update(Time timeElapsed)
        {
            mTreeControl.Update(timeElapsed);
            mFilePathTextBox.Update(timeElapsed);
            mEnterButton.Update(timeElapsed);
            mCloseButton.Update(timeElapsed);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mTreeControl, states);
            target.Draw(mFilePathTextBox, states);
            target.Draw(mEnterButton, states);
            target.Draw(mCloseButton, states);
        }
    }
}
