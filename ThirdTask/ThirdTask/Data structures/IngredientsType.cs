using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace ThirdTask
{
    [DataContract]
    [KnownType(typeof(Cucumbers))]
    [KnownType(typeof(MixMethod))]
    [KnownType(typeof(MixSaladDough))]
    public class IngredientsType<T> where T:Ingredients
    {
        [DataMember]
        private T[] _elements;

        public IngredientsType()
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

        /// <summary>
        /// Return number of elements in this structure
        /// </summary>
        public int Count { get { return _elements.Length; } }

        /// <summary>
        /// Add elements in structure
        /// </summary>
        /// <param name="ingredient"></param>
        public void Add(T ingredient)
        {
            var newSequence = new T[_elements.Length + 1];

            for (int i = 0; i < _elements.Length; i++)
            {
                newSequence[i] = _elements[i];
            }
            newSequence[_elements.Length] = ingredient;
            _elements = newSequence;
        }

        /// <summary>
        /// Delete all elements from structure
        /// </summary>
        public void Clear()
        {
            Array.Clear(_elements, 0, Count);
        }

        /// <summary>
        /// Find ingredient by its name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public T FindByName(string Name)
        {
            foreach(T element in _elements)
            {
                if (element.Name == Name)
                    return element;
            }
            return null;
        }

        /// <summary>
        /// Find ingredients by given tempreture condtition
        /// </summary>
        /// <param name="tempreture"></param>
        /// <returns></returns>
        public List<T> FindByCondition((int,int) tempreture)
        {
            List <T> elementsToReturn = new List<T> { }  ;
            foreach(T element in _elements)
            {
                if (element.StorageConditions == tempreture)
                    elementsToReturn.Add(element);
            }
            return elementsToReturn;
        }

        /// <summary>
        /// Find all ingredients that are in stock
        /// </summary>
        /// <returns></returns>
        public List<T> FindInStock()
        {
            List<T> elementsToReturn = new List<T> { };
            foreach (T element in _elements)
            {
                if (element.IsStock == true)
                    elementsToReturn.Add(element);
            }
            return elementsToReturn;
        }
        /// <summary>
        /// Calculate average usage of ingredients
        /// </summary>
        /// <param name="averageUsage"></param>
        /// <param name="averageWasted"></param>
        public void FindAverageUsage(out double averageUsage, out double averageWasted)
        {
            averageUsage = 0;
            averageWasted = 0;
            foreach(var element in _elements)
            {
                averageUsage += element.Usage;
                averageWasted += element.Wasted;
            }
            averageUsage /= _elements.Length;
            averageWasted /= _elements.Length;
        }

        /// <summary>
        /// Find most Usable ingredients
        /// </summary>
        /// <returns></returns>
        public List<T> FindMostUsableIngredients()
        {
            List<T> elementsToReturn = new List<T> { };
            double averageUsage;
            double averageWasted;
            FindAverageUsage(out averageUsage, out averageWasted);
            foreach(var elemet in _elements)
            {
                if (elemet.Usage > averageUsage && elemet.Wasted > averageWasted)
                    elementsToReturn.Add(elemet);
            }
            return elementsToReturn;
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
