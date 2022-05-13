using SFML.Graphics;
using SFML.System;
using System;

namespace SimplyCode.SFML.Games.ParticleSystem
{
    public class FadingParticle : ShapeParticle
    {
        public enum FadingState
        {
            FadingIn,
            Moving,
            FadingOut,
            Stopped
        }

        private const double MaxAlphaColor = 255;

        private double mAlpha = 0.0f;

        public FadingState State { get; protected set; } = FadingState.FadingIn;

        private Time mCurrent = Time.FromSeconds(0);

        public FadingParticle(Shape shape) : base(shape)
        {
            var color = Drawable.FillColor;
            color.A = 0;
            Drawable.FillColor = color;
            State = FadingState.FadingIn;
        }

        public TimeSpan EaseOut { get; set; } = TimeSpan.FromMilliseconds(200);
        public TimeSpan Move { get; set; } = TimeSpan.FromMilliseconds(500);
        public TimeSpan EaseIn { get; set; } = TimeSpan.FromMilliseconds(200);

        public override void Update(Time timeElapsed)
        {
            if (State == FadingState.Stopped)
            {
                return;
            }

            base.Update(timeElapsed);
            mCurrent += timeElapsed;

            if (State == FadingState.FadingIn)
            {
                if (EaseIn.TotalMilliseconds == 0)
                {
                    State = FadingState.Moving;
                    return;
                }
                var toAdd = (MaxAlphaColor / EaseIn.TotalSeconds) * timeElapsed.AsSeconds();
                var color = Drawable.FillColor;
                mAlpha += toAdd;
                color.A = (byte)mAlpha;
                Drawable.FillColor = color;

                if (mCurrent.AsMilliseconds() > EaseIn.TotalMilliseconds)
                {
                    State = FadingState.Moving;
                }
            }
            else if (State == FadingState.Moving)
            {
                if (Move.TotalMilliseconds == 0)
                {
                    State = FadingState.FadingOut;
                    return;
                }

                if (mCurrent.AsMilliseconds() > EaseIn.TotalMilliseconds + Move.TotalMilliseconds)
                {
                    State = FadingState.FadingOut;
                }
                var color = Drawable.FillColor;
                color.A = 255;
                Drawable.FillColor = color;
            }
            else if (State == FadingState.FadingOut)
            {
                if (EaseOut.TotalMilliseconds == 0)
                {
                    State = FadingState.Stopped;
                    return;
                }

                var toAdd = (MaxAlphaColor / EaseOut.TotalSeconds) * timeElapsed.AsSeconds();
                var color = Drawable.FillColor;
                mAlpha -= toAdd;
                color.A = (byte)mAlpha;
                Drawable.FillColor = color;

                if (mCurrent.AsMilliseconds() > EaseIn.TotalMilliseconds + Move.TotalMilliseconds + EaseOut.TotalMilliseconds)
                {
                    State = FadingState.Stopped;
                }
            }
        }
    }
}
