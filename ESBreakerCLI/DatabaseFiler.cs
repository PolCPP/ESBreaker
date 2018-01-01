using System;
using Contents;
using Contents.General;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Globalization;

namespace ESBreakerCLI
{
    public class DatabaseFiler
    {
        string dbDir = Path.Combine(System.IO.Path.GetDirectoryName(
                         Assembly.GetEntryAssembly().Location), "Databases");
        string saveDir = Path.Combine(System.IO.Path.GetDirectoryName(
                           Assembly.GetEntryAssembly().Location), "output");

        // Seriously what the heck is going on with those names? Security through obscurity? Please!
        readonly string[] directories = new string[]
        {
            "hOxogQ43AYuYE4T2QExzg5V5EhcQkxSV",
            "2cgR3iFOi4SuqNXs0wAYxkps4vI5Y6XB",
            "yq0QT0IqvC7s8Jxuy9AOdvsS93YSHHjX",
            "ToY1828HsafEBKw1Jdfi8443peG0CV4z",
            "t2I4C2vaaNAX5mq6ztKThAuSgejBWr8C"
        };

        // Note: We can't take "MASK_KEY" static vars from Contents.Base.Database because
        // it fails since it tries to run native android code via Android.Datapath
        // Right now we initialize it on LoadMask taking the one from Savedata.Filer which
        // happens to be the same. This may change on the future
        byte[] mask;
        string workFile;
        string saveFile;

        public DatabaseFiler(int fileType)
        {
            var fileDir = Path.Combine(dbDir, directories[fileType]);
            var saveFileDir = Path.Combine(saveDir, directories[fileType]);
            if (!Directory.Exists(fileDir))
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Directory {0} for this type of data does not exist:", fileDir));
            }
            // Their filename system way just makes my head hurt since
            // i doubt it's going to have more than a single file just pick the first one on each dir.
            string file =
                new DirectoryInfo(fileDir).GetFiles()
                  .Select(fi => fi.Name)
                  .FirstOrDefault();
            if (String.IsNullOrEmpty(file))
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "There isn't anything in the target directory {0}", fileDir));
            }
            workFile = Path.Combine(fileDir, file);
            saveFile = Path.Combine(saveFileDir, file);
            if (!Directory.Exists(saveFileDir))
            {
                Directory.CreateDirectory(saveFileDir);
            }

            // This is a temporal init way of the mask, using reflection against Savedata.Filer
            // which uses the same mask.
            // Since for this filer is legacy it may have to be replaced at some point with a
            // file with the key that won't be on the repo
            LoadMask();
        }

        public Format Load()
        {
            Format format = null;

            using (var stream = new FileStream(workFile, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                buffer = MaskBuffers(buffer);
                format = (Format)Serializer.DeserializeMemory(buffer, typeof(Format));
            }

            return format;
        }

        public void Save(Format format)
        {
            byte[] buffer = Serializer.SerializeMemory<Format>(format);
            buffer = MaskBuffers(buffer);
            using (var stream = new FileStream(saveFile, FileMode.Create))
            {
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        void LoadMask()
        {
            var field = typeof(SaveData.Filer).GetField("MASK_KEY",
                   BindingFlags.Static |
                   BindingFlags.NonPublic);
            mask = (byte[])field.GetValue(null);
        }

        byte[] MaskBuffers(byte[] _buffers)
        {
            byte[] buffer = new byte[_buffers.Length];
            for (int i = 0; i < _buffers.Length; i++)
            {
                buffer[i] = (byte)(_buffers[i] ^ mask[i % mask.Length]);
            }
            return buffer;
        }
    }
}
