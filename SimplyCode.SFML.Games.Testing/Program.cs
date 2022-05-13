using SFML.Graphics;
using SFML.Window;
using System;

namespace SimplyCode.SFML.Games.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(600, 400), "Testing", Styles.Resize);

            MyGame game = new MyGame(window, new FileGameResources());
            game.ChangeClearColor(Color.Cyan);
            game.GameLoop();
        }
    }
}
