using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SimplyCode.SFML.Games.Gui
{
    public interface IYesNoGuiWindow : IGuiWindow
    {
        public bool Result { get; set; }
    }


    public class MessageDialogWindow : IYesNoGuiWindow
    {
        public static bool Open(string title, string question)
        {
            var messageDialog = new MessageDialogWindow(title, question);

            messageDialog.Open();

            return messageDialog.Result;
        }



        private RenderWindow mWindow;
        private string mTitle = string.Empty;
        private string mQuestion = string.Empty;
        private Text mQuestionText;
        private TextButton mYesButton;
        private TextButton mNoButton;


        private MessageDialogWindow(string title, string question) 
        {
            mTitle = title;
            mQuestion = question;
        }

        public bool IsOpen => mWindow.IsOpen;
        public Vector2i Position { get => mWindow.Position; set => mWindow.Position = value; }
        public bool Result { get; set; }

        public void Open()
        {
            mWindow = new RenderWindow(VideoMode.DesktopMode, "TITLE HERE", Styles.None);


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


        public void Close()
        {
            mWindow.Close();
        }


        public void Dispose()
        {
            mWindow.Dispose();
            mWindow = null;
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
        }

        public void Update(Time timeElapsed)
        {
        }
    }
}
