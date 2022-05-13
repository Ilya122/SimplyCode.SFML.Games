using SFML.Graphics;
using SFML.System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class ShapeParticle : TParticle<Shape>
    {
        public ShapeParticle(Shape drawable) : base(drawable)
        {}
    }
}
