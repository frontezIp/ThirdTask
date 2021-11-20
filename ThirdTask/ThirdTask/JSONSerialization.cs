using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ThirdTask
{
    [DataContract]
    [KnownType(typeof(Cucumbers))]
    [KnownType(typeof(MixMethod))]
    [KnownType(typeof(MixSaladDough))]
    public class JSONSerialization :ISerialization
    {
        public void Save<T>(T[] data, string path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));
            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(filestream, data);
            }
        }

        public T[] Restore<T> (string filepath)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));
            T[] data;
            using (FileStream fileStream = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                T[] restoredData = (T[])jsonFormatter.ReadObject(fileStream);
                data = restoredData;
            }
            return data;
        }
    }
}
