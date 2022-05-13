using SimplyCode.SFML.Games.ParticleSystem;
using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.Testing
{
    public class MyGame : WindowGame
    {
        private RectangleShape mShape = null;
        private Shader mShaders;

        private SpawnerParticleSystem mSpawner;

        public MyGame(RenderWindow window, IGameResources resources) : base(window, resources)
        {
            mShape = new RectangleShape(new Vector2f(50, 50))
            {
                FillColor = Color.Black,
                Position = new Vector2f(100, 100),
                OutlineThickness = 5
            };

            mShaders = resources.GetShader("vertex.glsl", "fragment.glsl");

            mSpawner = new SpawnerParticleSystem()
            {
                SpawnPoint = new Vector2f(150, 250),
                SpawnPointDelta = 50,
                SpawnTime = Time.FromMilliseconds(100),
                Velocity = new Vector2f(0, -350f),
                EaseIn = TimeSpan.FromMilliseconds(0),
                EaseOut = TimeSpan.FromMilliseconds(100),
                Move = TimeSpan.FromMilliseconds(250),
                ShapreCreator = () => new CircleShape(5) { FillColor = Color.Yellow }
            };
        }

        protected override void Draw(RenderWindow window)
        {
            RenderStates states = new RenderStates(mShaders);

            window.Draw(mSpawner, states);

            //var verticies = new Vertex[]
            //{
            //    new Vertex{ Position  = new Vector2f(20,20), Color =Color.Black},
            //    new Vertex{ Position  = new Vector2f(80,50), Color =Color.Black},
            //    new Vertex{ Position  = new Vector2f(50,80), Color =Color.Black}
            //};
            //window.Draw(verticies, PrimitiveType.Points, states);

            //window.Draw(verticies, PrimitiveType.Points, statesNoShader);
        }

        protected override void Update(Time elapsed)
        {
            mSpawner.Update(elapsed);
        }
    }
}
