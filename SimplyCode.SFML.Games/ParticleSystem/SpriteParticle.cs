using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class SpriteParticle : TParticle<Sprite>
    {
        public SpriteParticle(Sprite drawable) : base(drawable)
        { }
    }
}
