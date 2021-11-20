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
    public class RecipesType<T> where T : Recipe
    {
        [DataMember]
        private T[] _elements;

        public RecipesType()
        {
            _elements = new T[0];
        }


        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return _elements[index];
            }
            set
            {
                _elements[index] = value;
            }
        }
        public int Count { get { return _elements.Length; } }

        public void Add(T operation)
        {
            var newSequence = new T[_elements.Length + 1];

            for (int i = 0; i < _elements.Length; i++)
            {
                newSequence[i] = _elements[i];
            }
            newSequence[_elements.Length] = operation;
            _elements = newSequence;
        }

        public void Clear()
        {
            Array.Clear(_elements, 0, Count);
        }

        /// <summary>
        /// Find recipe by its name and type
        /// </summary>
        /// <param name="NameOfDish"></param>
        /// <param name="typeOfDish"></param>
        /// <returns></returns>
        public T Find(string NameOfDish,string typeOfDish)
        {
            foreach (T element in _elements)
            {
                if (element.NameOfDish == NameOfDish && element.TypeOfDish == typeOfDish)
                    return element;
            }
            return null;
        }

        /// <summary>
        /// Serialize data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Serialize(string path, ISerialization serialization)
        {
            serialization.Save<T>(_elements, path);
        }

        /// <summary>
        /// Deserialize data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Deserialize(string path, ISerialization serialization)
        {
            _elements = serialization.Restore<T>(path);
        }


    }
}
