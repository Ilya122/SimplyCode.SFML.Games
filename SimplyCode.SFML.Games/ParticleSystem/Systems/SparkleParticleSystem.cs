using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public static class SparkleParticleSystem
    {
        private static Random mRandom = new Random();

        public static SpawnerParticleSystem Create(int randomTime = 200, int randomTimeDelta = 50, int circleSize = 5)
        {
            var randTime = mRandom.Next(randomTime - randomTimeDelta, randomTime + randomTimeDelta);

            Func<int> distrubutor = () => mRandom.Next(1, 5);

            return new SpawnerParticleSystem()
            {
                SpawnPoint = new Vector2f(150, 250),
                SpawnPointDelta = 75,
                SpawnTime = Time.FromMilliseconds(randTime),
                Velocity = new Vector2f(0, -700),
                EaseIn = TimeSpan.FromMilliseconds(0),
                EaseOut = TimeSpan.FromMilliseconds(50),
                Move = TimeSpan.FromMilliseconds(500),

                ShapreCreator = () => new CircleShape(circleSize)
                {
                    FillColor = new Color(255, 208, 89, 255)
                },
                AmountSpwanDistrubutor = distrubutor
            };
        }
    }
}

