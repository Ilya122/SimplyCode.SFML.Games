using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SimplyCode.SFML.Games.ParticleSystem
{

    public abstract class Particles<TParticle> : List<TParticle>, Drawable, IUpdatable, IDisposable where TParticle : IParticle
    {
        protected readonly object mLocker = new object();
        
        public virtual void Dispose()
        {
            foreach (var particle in this)
            {
                particle.Dispose();
            }
            Clear();
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            lock (mLocker)
            {
                foreach (var particle in this)
                {
                    target.Draw(particle, states);
                }
            }
        }

        public virtual void Update(Time timeElapsed)
        {
            lock (mLocker)
            {
                foreach (var particle in this)
                {
                    particle.Update(timeElapsed);
                }
            }
        }
    }
}
