using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTask
{
    public interface ISerialization
    {
        /// <summary>
        /// Save information to file using Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="path"></param>
        void Save<T>(T[] data, string path);
        /// <summary>
        /// Loads information from file using Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        T[] Restore<T>(string path);
    }
}
