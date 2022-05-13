using SimplyCode.SFML.Games.Abstract;
using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games
{
    public abstract class WindowGame : IGame, IRenderedGame
    {
        protected RenderWindow mWindow;
        protected IGameResources mGameResources;
        protected Color mClearColor;

        public WindowGame(RenderWindow existingWindow, IGameResources gameResources)
        {
            mWindow = existingWindow;
            mGameResources = gameResources;
            mClearColor = Color.White;
        }

        public virtual void InitializeComponents()
        {
        }

        public void GameLoop()
        {
            InitializeComponents();
            Clock clock = new Clock();

            while (mWindow.IsOpen)
            {
                mWindow.Clear(mClearColor);

                var last = clock.Restart();
                Update(last);
                Draw(mWindow);

                mWindow.DispatchEvents();
                mWindow.Display();
            }
        }

        protected abstract void Update(Time elapsed);

        protected abstract void Draw(RenderWindow window);

        public Color CurrentClearColor => mClearColor;


        public void ChangeClearColor(Color color)
        {
            mClearColor = color;
        }
    }
}