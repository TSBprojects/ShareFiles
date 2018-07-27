using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [Serializable]
    public class NetFiles : IEnumerable<NetFile>
    {
        public NetFiles()
        {
            Files = new List<NetFile>();
        }

        public NetFiles(byte[] data)
        {
            Files = FromArray(data).GetFiles();
        }


        private List<NetFile> Files;

        public List<NetFile> GetFiles()
        {
            return Files;
        }

        public void Add(NetFile file)
        {
            Files.Add(file);
        }


        public byte[] ToArray()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);

                return stream.ToArray();
            }
        }

        public static NetFiles FromArray(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(data))
            {
                stream.Position = 0;
                return (NetFiles)formatter.Deserialize(stream);
            }
        }

        public IEnumerator<NetFile> GetEnumerator()
        {
            return Files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Files.GetEnumerator();
        }
    }
}
