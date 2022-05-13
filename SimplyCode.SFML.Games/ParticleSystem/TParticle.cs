using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class TParticle<TDrawable> : IParticle where TDrawable : Transformable, Drawable, IDisposable
    {
        public TParticle(TDrawable drawable)
        {
            Drawable = drawable;
        }

        public TDrawable Drawable { get; protected set; }

        public Vector2f Velocity { get; set; }

        public bool ShouldDispose { get; set; }

        public virtual void Dispose()
        {
            if (ShouldDispose)
            {
                Drawable.Dispose();
            }
            Drawable = null;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Drawable, states);
        }

        public virtual void Update(Time timeElapsed)
        {
            Drawable.Position += (Velocity * timeElapsed.AsSeconds());
        }
    }
}
