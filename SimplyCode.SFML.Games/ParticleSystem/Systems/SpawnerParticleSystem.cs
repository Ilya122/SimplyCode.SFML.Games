using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class SpawnerParticleSystem : Particles<FadingParticle>
    {
        private readonly Random mRandom = new Random();
        private Time mTotalTimeElapsed;

        public Vector2f SpawnPoint { get; set; }
        //TODO: Can be upgraded to  interface to generate new spawn points
        public int SpawnPointDelta { get; set; }

        public Time SpawnTime { get; set; }

        public Vector2f Velocity { get; set; }
        public TimeSpan EaseOut { get; set; } = TimeSpan.FromMilliseconds(200);
        public TimeSpan Move { get; set; } = TimeSpan.FromMilliseconds(500);
        public TimeSpan EaseIn { get; set; } = TimeSpan.FromMilliseconds(200);
        public Func<Shape> ShapreCreator { get; set; } = () => new CircleShape(10);
        public Func<int> AmountSpwanDistrubutor { get; set; } = () => 1;

        public override void Update(Time timeElapsed)
        {
            base.Update(timeElapsed);
            mTotalTimeElapsed += timeElapsed;
            if (mTotalTimeElapsed >= SpawnTime)
            {
                var amount = AmountSpwanDistrubutor();
                for (int i = 0; i < amount; i++)
                {

                    var shape = ShapreCreator();

                    var x = mRandom.Next(-1 * SpawnPointDelta, SpawnPointDelta);
                    shape.Position = new Vector2f(SpawnPoint.X + x, SpawnPoint.Y);
                    var particle = new FadingParticle(shape)
                    {
                        Velocity = Velocity,
                        ShouldDispose = true,
                        EaseIn = EaseIn,
                        EaseOut = EaseOut,
                        Move = Move,
                    };
                    Add(particle);
                }

                mTotalTimeElapsed = Time.FromMicroseconds(0);
            }

            List<FadingParticle> toRemoves = new List<FadingParticle>();
            foreach (var particle in this.Where(p => p.State == FadingParticle.FadingState.Stopped))
            {
                toRemoves.Add(particle);
            }
            foreach (var toRemove in toRemoves)
            {
                Remove(toRemove);
            }
        }
    }
}

