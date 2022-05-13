using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimplyCode.SFML.Games
{
    public class FileGameResources : IGameResources
    {
        private readonly Dictionary<string, Sound> mCachedSounds = new Dictionary<string, Sound>();
        private readonly Dictionary<string, Texture> mCachedTextures = new Dictionary<string, Texture>();
        private readonly Dictionary<string, Font> mCachedFonts = new Dictionary<string, Font>();
        private readonly Dictionary<string, Shader> mCachedShaders = new Dictionary<string, Shader>();
        private readonly string mResourcesBaseDirectory;

        public FileGameResources(string resourcesBaseDir = "Resources")
        {
            mResourcesBaseDirectory = resourcesBaseDir;
        }

        public Sound GetSound(string id)
        {
            return GetSound("Sounds", id);
        }

        public Texture GetTexture(string id)
        {
            return GetResource(mCachedTextures, "Textures", id);
        }

        public Font GetFont(string id)
        {
            return GetResource(mCachedFonts, "Fonts", id);
        }

        public Shader GetShader(string id, ShaderType type)
        {
            return GetShader("Shaders", id, type);
        }

        //TODO: Bug - if vertexID are same but fragmentID are different it returns the same shader
        public Shader GetShader(string vertexId, string fragmentId)
        {
            return GetShaderBase("Shaders", vertexId, fragmentId);
        }

        public Shader GetShader(string vertexId, string fragmentId, string geometryId)
        {
            return GetShaderBase("Shaders", vertexId, fragmentId, geometryId);
        }

        private T GetResource<T>(Dictionary<string, T> resourcesCache, string baseResourceDir, string id)
        {
            if (resourcesCache.ContainsKey(id))
            {
                return resourcesCache[id];
            }

            var fullPath = Path.Combine(mResourcesBaseDirectory, baseResourceDir, id);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {id}");
            }
            var resources = (T)Activator.CreateInstance(typeof(T), fullPath);
            resourcesCache.Add(id, resources);
            return resources;
        }

        private Sound GetSound(string baseResourceDir, string id)
        {
            if (mCachedSounds.ContainsKey(id))
            {
                return mCachedSounds[id];
            }

            var fullPath = Path.Combine(mResourcesBaseDirectory, baseResourceDir, id);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {id}");
            }
            var sound = new Sound(new SoundBuffer(fullPath));
            mCachedSounds.Add(id, sound);
            return sound;
        }

        private Shader GetShader(string baseResourceDir, string id, ShaderType type)
        {
            if (mCachedShaders.ContainsKey(id))
            {
                return mCachedShaders[id];
            }
            var fullPath = Path.Combine(mResourcesBaseDirectory, baseResourceDir, id);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {id}");
            }

            Shader shader = null;
            if (type == ShaderType.Vertex)
            {
                shader = new Shader(fullPath, null, null);
            }
            else if (type == ShaderType.Geometry)
            {
                shader = new Shader(null, fullPath, null);
            }
            else if (type == ShaderType.Fragment)
            {
                shader = new Shader(null, null, fullPath);
            }

            mCachedShaders[id] = shader;
            return shader;
        }

        private Shader GetShaderBase(string baseResourceDir, string vertexId, string fragmentId, string geometryId = "")
        {
            var geometryRequested = !string.IsNullOrEmpty(geometryId);
            if (mCachedShaders.ContainsKey(vertexId))
            {
                return mCachedShaders[vertexId];
            }
            if (mCachedShaders.ContainsKey(fragmentId))
            {
                return mCachedShaders[fragmentId];
            }
            if (geometryRequested && mCachedShaders.ContainsKey(geometryId))
            {
                return mCachedShaders[geometryId];
            }

            var workingDirectory = Environment.CurrentDirectory;
            var vertexFullPath = Path.Combine(workingDirectory, mResourcesBaseDirectory, baseResourceDir, vertexId);
            var fragmentFullPath = Path.Combine(workingDirectory, mResourcesBaseDirectory, baseResourceDir, fragmentId);
            var geometryFullPath = Path.Combine(workingDirectory, mResourcesBaseDirectory, baseResourceDir, geometryId);
            if (!File.Exists(vertexFullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {vertexId}");
            }
            if (!File.Exists(fragmentFullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {fragmentId}");
            }

            if (geometryRequested && !File.Exists(fragmentFullPath))
            {
                throw new FileNotFoundException($"Cannot find {baseResourceDir} with the id of: {fragmentId}");
            }

            var vertexShader = File.ReadAllText(vertexFullPath);
            var fragmentShader = File.ReadAllText(fragmentFullPath);
            var geometryShader = geometryRequested ? File.ReadAllText(geometryFullPath) : null;

            Shader shader = Shader.FromString(vertexShader, geometryShader, fragmentShader);
            mCachedShaders[vertexId] = shader;
            mCachedShaders[fragmentId] = shader;
            return shader;
        }
    }
}