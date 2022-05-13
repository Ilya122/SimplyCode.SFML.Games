using SFML.System;

namespace SimplyCode.SFML.Games.ParticleSystem
{

    public class RandomFloatingParticles : Particles<RotatingParticle>
    {
        private readonly int mWidthSize;
        private readonly int mHeightSize;

        public RandomFloatingParticles(int width, int height)
        {
            mWidthSize = width;
            mHeightSize = height;
        }

        public override void Update(Time timeElapsed)
        {
            foreach (var particle in this)
            {
                if (particle.Drawable.Position.X > mWidthSize || particle.Drawable.Position.X < 0)
                {
                    particle.Velocity *= -1;
                }

                if (particle.Drawable.Position.Y > mHeightSize || particle.Drawable.Position.Y < 0)
                {
                    particle.Velocity *= -1;
                }


                particle.Update(timeElapsed);
            }
        }
    }
}
