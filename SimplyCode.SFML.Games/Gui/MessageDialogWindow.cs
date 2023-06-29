using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

namespace SimplyCode.SFML.Games.Gui
{
    public enum MessageDialogButtons
    {
        YesNo,
        Ok,
        Exit,
        Custom
    }

    public interface IDialogWindow : IGuiWindow
    {
        public MessageDialogButtons ButtonTypes { get; }
    }


    public class MessageDialogWindow : IDialogWindow
    {
        public static bool Open(RenderWindow parentWindow, Font fontToUse, string title, string text, MessageDialogButtons buttons = MessageDialogButtons.Ok)
        {
            using var messageDialog = new MessageDialogWindow(parentWindow, fontToUse, title, text, buttons);
            messageDialog.Open();

            return messageDialog.Result;
        }

        private readonly string mTitle = string.Empty;
        private readonly string mText = string.Empty;

        // GUI Related:
        private readonly RenderWindow mParentWindow;
        private RenderWindow mWindow;
        private readonly List<TextButton> mButtons = new List<TextButton>();
        private Text mContextText;
        private readonly Font mUsedFont;

        private const int TextSize = 24;

        // TODO: Font DI - Use a more complete system so the user doesn't have to provide the font.
        private MessageDialogWindow(RenderWindow parent, Font font, string title, string text, MessageDialogButtons buttons)
        {
            mParentWindow = parent;
            mTitle = title;
            mText = text;
            ButtonTypes = buttons;
            mUsedFont = font;
        }

        public bool IsOpen => mWindow.IsOpen;
        public Vector2i Position { get => mWindow.Position; set => mWindow.Position = value; }
        public bool Result { get; set; }
        public MessageDialogButtons ButtonTypes { get; private set; }

        public void Open()
        {
            mWindow = new RenderWindow(new VideoMode { Width = 300, Height = 100, BitsPerPixel = 24 }, mTitle, Styles.Titlebar)
            {
                Position = mParentWindow.Position + new Vector2i((int)mParentWindow.Size.X / 2, (int)mParentWindow.Size.Y / 2)
            };



            mButtons.AddRange(CreateButtons(mWindow, mUsedFont, ButtonTypes));

            mContextText = new Text(mText, mUsedFont, TextSize)
            {
                Position = new Vector2f(0, 0),
                FillColor = Color.Black
            };

            mContextText.CenterX(mWindow);

            Clock clock = new Clock();
            var elapsed = clock.Restart();

            mWindow.RequestFocus();

            while (mWindow.IsOpen)
            {
                mWindow.RequestFocus();

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
            foreach (var button in mButtons)
            {
                target.Draw(button, states);
            }
            target.Draw(mContextText, states);
        }

        public void Update(Time timeElapsed)
        {
            foreach (var button in mButtons)
            {
                button.Update(timeElapsed);
            }
        }

        private IEnumerable<TextButton> CreateButtons(RenderWindow window, Font fontToUse, MessageDialogButtons buttons)
        {
            uint textSize = TextSize;
            //TODO: Gui size to 
            switch (buttons)
            {
                case MessageDialogButtons.Ok:
                    var okText = new Text("Ok", fontToUse, textSize)
                    {
                        FillColor = Color.Black
                    };

                    var textLocBounds = okText.GetLocalBounds();
                    var textGlobBounds = okText.GetGlobalBounds();

                    okText.Position = new Vector2f(
                            window.Size.X / 2 - textGlobBounds.Width / 2,
                            window.Size.Y - (float)(textGlobBounds.Height * 2));

                    var tBut = new TextButton(okText, window);

                    tBut.OnPress += OnOkPress;

                    yield return tBut;

                    break;

                case MessageDialogButtons.YesNo:
                    yield return new TextButton(new Text("Yes", fontToUse, textSize)
                    {
                        FillColor = Color.Black
                    }, window)
                    { Position = new Vector2f() };

                    yield return new TextButton(new Text("No", fontToUse, textSize)
                    {
                        FillColor = Color.Black
                    }, window)
                    { Position = new Vector2f() };
                    break;

                case MessageDialogButtons.Exit:
                    yield return new TextButton(new Text("Exit", fontToUse, textSize)
                    {
                        FillColor = Color.Black
                    }, window)
                    { Position = new Vector2f() };
                    break;

                case MessageDialogButtons.Custom:
                    break;
            }
        }

        private void OnOkPress()
        {
            mWindow.Close();
        }
    }
}
