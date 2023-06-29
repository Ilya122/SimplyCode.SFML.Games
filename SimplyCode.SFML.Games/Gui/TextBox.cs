using SimplyCode.SFML.Games.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SimplyCode.SFML.Games.Gui
{
    // TODO: Implement Bounding Box
    // TODO: Implement Focus so it won't always write
    // TODO: Validations and Regex? (Show green/red borders when valid/fail)
    public class TextBox : IGuiControl
    {
        #region Privates 
        private Text mTextControl;
        private Text mLabel;
        private Sprite mCursor;
        private int mCursorIndex = 0;

        private Sprite mBoundingBox;
        private Window mParent;

        private const int LineWidth = 2;
        private const int CursorWidth = 3;
        private readonly Vector2f BoundingBoxPositionDiff = new Vector2f(5, 5);

        private static Dictionary<Keyboard.Key, string> mKeys;
        #endregion

        #region Ctor
        static TextBox()
        {
            mKeys = new Dictionary<Keyboard.Key, string>
            {
                { Keyboard.Key.A , "A" },
                { Keyboard.Key.B , "B" },
                { Keyboard.Key.C , "C" },
                { Keyboard.Key.D , "D" },
                { Keyboard.Key.E , "E" },
                { Keyboard.Key.F , "F" },
                { Keyboard.Key.G , "G" },
                { Keyboard.Key.H , "H" },
                { Keyboard.Key.I , "I" },
                { Keyboard.Key.J , "J" },
                { Keyboard.Key.K , "K" },
                { Keyboard.Key.L , "L" },
                { Keyboard.Key.M , "M" },
                { Keyboard.Key.N , "N" },
                { Keyboard.Key.O , "O" },
                { Keyboard.Key.P , "P" },
                { Keyboard.Key.Q , "Q" },
                { Keyboard.Key.R , "R" },
                { Keyboard.Key.S , "S" },
                { Keyboard.Key.T , "T" },
                { Keyboard.Key.U , "U" },
                { Keyboard.Key.V , "V" },
                { Keyboard.Key.W , "W" },
                { Keyboard.Key.X , "X" },
                { Keyboard.Key.Y , "Y" },
                { Keyboard.Key.Z , "Z" },
                { Keyboard.Key.Num0 , "0" },
                { Keyboard.Key.Num1 , "1" },
                { Keyboard.Key.Num2 , "2" },
                { Keyboard.Key.Num3 , "3" },
                { Keyboard.Key.Num4 , "4" },
                { Keyboard.Key.Num5 , "5" },
                { Keyboard.Key.Num6 , "6" },
                { Keyboard.Key.Num7 , "7" },
                { Keyboard.Key.Num8 , "8" },
                { Keyboard.Key.Num9 , "9" },
                { Keyboard.Key.Semicolon , ";" },
                { Keyboard.Key.Comma , "," },
                { Keyboard.Key.Period , "." },
                { Keyboard.Key.Quote , "\"" },
                { Keyboard.Key.Slash , "/" },
                { Keyboard.Key.Backslash , "\\" },
                { Keyboard.Key.Tilde , "~" },
                { Keyboard.Key.Equal , "=" },
                { Keyboard.Key.Hyphen , "-" },
                { Keyboard.Key.Space , " " },
                { Keyboard.Key.Tab , "	" },
                { Keyboard.Key.Add , "+" },
                { Keyboard.Key.Subtract , "-" },
                { Keyboard.Key.Multiply , "*" },
                { Keyboard.Key.Divide , "/" },
                { Keyboard.Key.Numpad0 , "0" },
                { Keyboard.Key.Numpad1 , "1" },
                { Keyboard.Key.Numpad2 , "2" },
                { Keyboard.Key.Numpad3 , "3" },
                { Keyboard.Key.Numpad4 , "4" },
                { Keyboard.Key.Numpad5 , "5" },
                { Keyboard.Key.Numpad6 , "6" },
                { Keyboard.Key.Numpad7 , "7" },
                { Keyboard.Key.Numpad8 , "8" },
                { Keyboard.Key.Numpad9 , "9" }
            };

        }

        public TextBox(Window parent, Font font, uint fontSize, Vector2f position, Vector2u size, string labelToWriteOnEmtpy = "")
        {
            mTextControl = new Text(string.Empty, font)
            {
                CharacterSize = fontSize,
                FillColor = Color.Black,
            };
            mLabel = new Text(string.Empty, font)
            {
                CharacterSize = fontSize,
                FillColor = Color.Black
            };

            var mCursorText = TextureFactory.CreateTexture(CursorWidth, fontSize, Color.Black);
            mCursor = new Sprite(mCursorText);

            mParent = parent;
            mTextControl.Position = position;
            mLabel.Position = position;
            mCursor.Position = position;

            mParent.KeyPressed += KeyPressed;
            mParent.MouseButtonPressed += MousePressed;

            var boundTexYSize = fontSize + (fontSize);

            var boundTex = TextureFactory.CreateHollowTexture(size.X, boundTexYSize, Color.Black, LineWidth);
            mBoundingBox = new Sprite(boundTex);
            mBoundingBox.Position = position - BoundingBoxPositionDiff;
        }
        #endregion

        #region Properties
        public Vector2f Position
        {
            get { return mTextControl.Position; }
            set { ReadjustPosition(mTextControl.Position, value); }
        }

        public FloatRect Bounds { get => mTextControl.GetGlobalBounds(); }

        public bool IsReadOnly { get; set; }
        public bool IsFocused { get; set; }
        public bool Enabled { get; set; }

        public string Text
        {
            get { return mTextControl.DisplayedString; }
            set
            {
                mTextControl.DisplayedString = value;
                SetCursorIndex(value);
            }
        }

        public int MaxCharacterSize { get; set; } = -1;
        #endregion


        public void Draw(RenderTarget target, RenderStates states)
        {
            if (mTextControl.DisplayedString.Length == 0)
            {
                target.Draw(mLabel, states);
            }
            else
            {
                target.Draw(mTextControl, states);
            }
            if (!IsReadOnly && IsFocused)
            {
                target.Draw(mCursor, states);
            }
            target.Draw(mBoundingBox, states);
        }

        public void Update(Time timeElapsed)
        {
        }

        private void ReadjustPosition(Vector2f oldPosition, Vector2f newPosition)
        {
            var diff = oldPosition - newPosition;
            mTextControl.Position = newPosition;
            mCursor.Position = newPosition;
            mLabel.Position = newPosition;
            mBoundingBox.Position = newPosition - BoundingBoxPositionDiff;

            SetCursorIndex(mTextControl.DisplayedString);
        }

        private void MousePressed(object sender, MouseButtonEventArgs e)
        {
            var bounds = mBoundingBox.GetGlobalBounds();
            IsFocused = bounds.Contains(e.X, e.Y);
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!IsFocused)
            {
                return;
            }
            if (IsReadOnly)
            {
                return;
            }

            if (mKeys.ContainsKey(e.Code))
            {
                var str = mKeys[e.Code];
                if (!e.Shift)
                {
                    str = str.ToLower();
                } 
                else
                {
                    //TODO: Map to dictionary properly with shift support and ctrl support to enable special characters.
                    if (e.Code == Keyboard.Key.Semicolon)
                    {
                        str = ":";
                    }
                }


                mTextControl.DisplayedString += str;

                if (mCursorIndex < mTextControl.DisplayedString.Length - 1)
                {
                    if (mCursorIndex < mTextControl.DisplayedString.Length - 1)
                    {
                        var firstHalf = mCursorIndex == 0 ? string.Empty : mTextControl.DisplayedString.Substring(0, mCursorIndex);
                        var second = mTextControl.DisplayedString.Substring(mCursorIndex, mTextControl.DisplayedString.Length - 1);
                        mTextControl.DisplayedString = $"{firstHalf}{str}{second}";
                    }
                }
                IncrementCursorIndex();
            }
            else if (e.Code == Keyboard.Key.Backspace && mTextControl.DisplayedString.Length > 0)
            {
                var left = mTextControl.DisplayedString.Substring(0, mTextControl.DisplayedString.Length - 1);
                mTextControl.DisplayedString = left;
                DecreaseCursorIndex();
            }
            else if (e.Code == Keyboard.Key.Delete && mTextControl.DisplayedString.Length > 0)
            {
                if (mCursorIndex < mTextControl.DisplayedString.Length)
                {
                    var firstHalf = mCursorIndex == 0 ? string.Empty : mTextControl.DisplayedString.Substring(0, mCursorIndex);
                    var second = mTextControl.DisplayedString.Substring(mCursorIndex + 1, mTextControl.DisplayedString.Length - 1);
                    mTextControl.DisplayedString = firstHalf + second;
                }
            }
            else if (e.Code == Keyboard.Key.Right)
            {
                if (mCursorIndex <= mTextControl.DisplayedString.Length - 1)
                {
                    IncrementCursorIndex();
                }
            }
            else if (e.Code == Keyboard.Key.Left)
            {
                if (mCursorIndex > 0)
                {
                    DecreaseCursorIndex();
                }
            }
        }

        private void IncrementCursorIndex()
        {
            ++mCursorIndex;
            var pos = mTextControl.FindCharacterPos((uint)mCursorIndex);
            mCursor.Position = new Vector2f(mTextControl.Position.X +  pos.X, mCursor.Position.Y);
        }

        private void DecreaseCursorIndex()
        {
            --mCursorIndex;
            var pos = mTextControl.FindCharacterPos((uint)mCursorIndex);
            mCursor.Position = new Vector2f(mTextControl.Position.X + pos.X, mCursor.Position.Y);

            if (mCursorIndex < 0)
            {
                mCursorIndex = 0;
            }
        }

        private void SetCursorIndex(string newString)
        {
            mCursorIndex = newString.Length - 1;
            mCursorIndex = mCursorIndex < 0 ? 0 : mCursorIndex;

            var length = mTextControl.DisplayedString.Length == 0 ? 1 : mTextControl.DisplayedString.Length;
            var letterSpacing = (int)(mTextControl.GetGlobalBounds().Width / length);

            mCursor.Position = new Vector2f(mTextControl.Position.X + letterSpacing, mTextControl.Position.Y);
        }

        public void Dispose()
        {
            mTextControl.Dispose();
            mTextControl = null;
            mCursor.Dispose();
            mCursor = null;
            mLabel.Dispose();
            mLabel = null;
            mBoundingBox.Dispose();
            mBoundingBox = null;

            mParent.KeyReleased -= KeyPressed;
            mParent = null;
        }

    }
}