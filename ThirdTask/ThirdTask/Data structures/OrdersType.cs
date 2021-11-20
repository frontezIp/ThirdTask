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
    public class OrdersType<T> where T: Order
    {
        [DataMember]
        private T[] _orders;

        public OrdersType()
        {
            _orders = new T[0];
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
                return _orders[index];
            }
            set
            {
                _orders[index] = value;
            }
        }
        /// <summary>
        /// Return elements of orders in this data structure
        /// </summary>
        public int Count { get { return _orders.Length; } }

        /// <summary>
        /// Add element to data
        /// </summary>
        /// <param name="operation"></param>
        public void Add(T operation)
        {
            var newSequence = new T[_orders.Length + 1];

            for (int i = 0; i < _orders.Length; i++)
            {
                newSequence[i] = _orders[i];
            }
            newSequence[_orders.Length] = operation;
            _orders = newSequence;
        }

        /// <summary>
        /// Deletes all the elements in data
        /// </summary>
        public void Clear()
        {
            Array.Clear(_orders, 0, Count);
        }

        /// <summary>
        /// Returns all order that has been made in given period of time
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<T> FindOrderForGinenPeriod(DateTime from, DateTime to)
        {
            List<T> elements = new List<T> { };
            foreach(var order in _orders)
            {
                if(order.Time>= from&& order.Time <= to)
                {
                    elements.Add(order);
                }
            }
            return elements;
        }

        /// <summary>
        /// Serialize orders
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Serialize(string path, ISerialization serialization)
        {
            serialization.Save<T>(_orders, path);
        }

        /// <summary>
        /// Desearilize orders
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Deserialize(string path, ISerialization serialization)
        {
            _orders = serialization.Restore<T>(path);
        }
    }
}
