using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace De.HsFlensburg.ClientApp051.Services.SerializationService
{
    public class ModelFileHandler
    {
        public BookManager ReadModelFromFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream streamLoad = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
            var loadedManager =
                (BookManager)formatter.Deserialize(streamLoad);
            streamLoad.Close();
            return loadedManager;
        }

        public void WriteModelToFile(
            string path,
            BookManager model)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);
            formatter.Serialize(stream, model);
            stream.Close();
        }
    }
}