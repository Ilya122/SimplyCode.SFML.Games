using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SimplyCode.SFML.Games.Saving
{
    public class FileRotater
    {
        private string mCurrentNumber;
        private IEnumerable<string> mFiles;

        private static string Prefix = "save_";
        private static string Extension = ".data1";

        private FileRotater(string currNumber, IEnumerable<string> files)
        {
            mCurrentNumber = currNumber;
            mFiles = files;
        }

        public static FileRotater Create(string folderPath)
        {
            Regex fileNameRegex = new Regex($"{Prefix}([0-9]+){Extension}");

            var files = Directory.GetFiles(folderPath, $"{Prefix}_*{Extension}");
            var maxNum = 0;
            foreach (var file in files)
            {
                var match = fileNameRegex.Match(file);
                if (!match.Success)
                    continue; // Not ours...

                var group = match.Groups[1];
                var val = group.Value;
                var num = int.Parse(val);
                if (num > maxNum)
                {
                    maxNum = num;
                }
            }

            return new FileRotater(maxNum.ToString(), files);
        }

        public string CurrentFileName { get => $"{Prefix}{mCurrentNumber}{Extension}"; }
    }


    public class BinarySaveArrange : ISaveArrange, ILoadArrange
    {
        private IDictionary<string, IEntitySerialized> mSerialization = new Dictionary<string, IEntitySerialized>();
        private IDictionary<string, IEntityDeserialized> mDeserialization = new Dictionary<string, IEntityDeserialized>();
        private FileStream mStream;

        public string PathToSavedFilesFolder { get; set; }

        private BinarySaveArrange() { }

        //TODO: Path to be controlled by settings. (So installation can be done).
        //TODO: A way to handle multiple arrangments (One save arrange = one file)
        public static BinarySaveArrange Begin()
        {
            var shortAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            var tempPath = Path.Combine(Path.GetTempPath(), shortAssemblyName, "savedfiles");

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            return new BinarySaveArrange() { PathToSavedFilesFolder = tempPath };
        }
        public static BinarySaveArrange Begin(string tempPath)
        {
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            return new BinarySaveArrange() { PathToSavedFilesFolder = tempPath };
        }

        public ISaveArrange Save<T>(T item, string id) where T : IEntitySerialized
        {
            mSerialization.Add(id, item);
            return this;
        }

        public void Pack()
        {
            //TODO: Roll files and save metadata on saved files.
            using var fs = CreateOrGet(FileMode.Create);

            foreach (var item in mSerialization)
            {
                item.Value.SerializeInto(fs);
            }

            mSerialization.Clear();
        }

        public ILoadArrange Load<T>(string id) where T : IEntityDeserialized, new()
        {
            using var fs = CreateOrGet();
            var val = new T();
            val.DeserializeFrom(fs);
            mDeserialization.Add(id, val);

            return this;
        }

        public ILoadArrange Load<T>(string id, params object[] ctorArguments) where T : IEntityDeserialized
        {
            var fs = CreateOrGet();

            var ctor = typeof(T).GetConstructor(ctorArguments.Select(t => t.GetType()).ToArray());
            var val = (IEntitySerialization)ctor.Invoke(ctorArguments);

            val.DeserializeFrom(fs);
            mDeserialization.Add(id, val);

            return this;
        }

        public IUnpackedParameters Unpack()
        {
            var unpacker = new UnpackedParameters(mDeserialization);

            mSerialization = null;
            using var fs = CreateOrGet();

            return unpacker;
        }

        private FileStream CreateOrGet(FileMode mode = FileMode.Open)
        {
            if (mStream == null)
            {
                var fullPath = Path.Combine(PathToSavedFilesFolder, "x.savedData");

                mStream = new FileStream(fullPath, mode);
            }
            return mStream;
        }


    }
}
