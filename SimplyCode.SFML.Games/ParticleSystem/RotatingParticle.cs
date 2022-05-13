using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class RotatingParticle : SpriteParticle
    {
        private readonly float mRotationSpeed = 5f;

        public RotatingParticle(float rotationSpeed, Texture tex)
            : base(new Sprite(tex))
        {
            mRotationSpeed = rotationSpeed;
        }

        public override void Update(Time timeElapsed)
        {
            base.Update(timeElapsed);

            if (Drawable.Rotation > 360f)
            {
                Drawable.Rotation = -1 * mRotationSpeed;
                Drawable.Rotation += (mRotationSpeed * timeElapsed.AsSeconds());
            }
            else
            {
                Drawable.Rotation += (mRotationSpeed * timeElapsed.AsSeconds());
            }

        }
    }
}
