using SFML.Graphics;
using System.IO;
using System.Reflection;

namespace SimplyCode.SFML.Games.Resources
{
    /// <summary>
    /// Internal resources for the SimplyCode.SFML.Games library.
    /// </summary>
    public static class EmbeddedResourcesUtilities
    {
        public static Texture ReadTexture(string resourceName)
        {
            //From the assembly where this code lives!
            //or from the entry point to the application - there is a difference!
            //  var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream($"SimplyCode.SFML.Games.Resources.{resourceName}"))
            {
                return new Texture(stream);
            }
        }
    }
}
