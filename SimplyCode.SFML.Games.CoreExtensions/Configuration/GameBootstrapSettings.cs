namespace SimplyCode.SFML.Games.CoreExtensions.Configuration
{
    /// <summary>
    /// Setting that are applied before game is opened.
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public abstract class GameBootstrapSettings<TConfig>
    {
        protected TConfig mDefaultConfig;
        protected bool mHasDefault = false;
        public abstract TConfig Get();
        public abstract void Set(TConfig config);

        public virtual void SetDefault(TConfig config)
        {
            mDefaultConfig = config;
            mHasDefault = true;
        }
    }
}