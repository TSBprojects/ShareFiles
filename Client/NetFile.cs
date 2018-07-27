using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [Serializable]
    public class NetFile
    {
        public NetFile() { }

        public NetFile(byte[] data)
        {
            NetFile file = FromArray(data);
            FileName = file.FileName;
            Data = file.Data;
        }


        public string FileName { get; set; }

        public byte[] Data { get; set; }


        public byte[] ToArray()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);

                return stream.ToArray();
            }
        }

        public static NetFile FromArray(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(data))
            {
                stream.Position = 0;
                return (NetFile)formatter.Deserialize(stream);
            }
        }
    }
}
