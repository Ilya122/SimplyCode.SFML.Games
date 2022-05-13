using System.IO;
using System.Text.Json;

namespace SimplyCode.SFML.Games.CoreExtensions.Configuration
{
    public class ApplicationSettings<TConfigObject> : GameBootstrapSettings<TConfigObject>
    {
        private readonly string SettingsFile = "Settings.json";


        public override TConfigObject Get()
        {
            if (!File.Exists(SettingsFile) && mHasDefault)
            {
                return mDefaultConfig;
            }

            CreateNotExists();

            using StreamReader file = new StreamReader(SettingsFile);
            var content = file.ReadToEnd();
            return JsonSerializer.Deserialize<TConfigObject>(content);
        }

        public override void Set(TConfigObject config)
        {
            CreateNotExists();

            using StreamWriter file = new StreamWriter(SettingsFile);
            var json = JsonSerializer.Serialize(config);
            file.Write(json);
        }

        private void CreateNotExists()
        {
            if (!File.Exists(SettingsFile))
            {
                using var file = File.Create(SettingsFile);
            }
        }
    }
}
