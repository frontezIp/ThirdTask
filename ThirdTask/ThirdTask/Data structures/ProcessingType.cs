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
    public class ProcessingType<T> where T: IProcessingMethod 
    {
        [DataMember]
        private T[] _processings;

        public ProcessingType()
        {
            _processings = new T[0];
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return _processings[index];
            }
            set
            {
                _processings[index] = value;
            }
        }

        /// <summary>
        /// returns number of processings
        /// </summary>
        public int Count { get { return _processings.Length; } }

        /// <summary>
        /// Add processings method
        /// </summary>
        /// <param name="operation"></param>
        public void Add(T operation)
        {
            var newSequence = new T[_processings.Length + 1];

            for (int i = 0; i < _processings.Length; i++)
            {
                newSequence[i] = _processings[i];
            }
            newSequence[_processings.Length] = operation;
            _processings = newSequence;
        }

        /// <summary>
        /// Deletes all processings method
        /// </summary>
        public void Clear()
        {
            Array.Clear(_processings, 0, Count);
        }

        /// <summary>
        /// Find operation by given type and type of dish to process
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeOfDish"></param>
        /// <returns></returns>
        public IOperation findOperation(string type,string typeOfDish)
        {
            foreach(var element in _processings)
            {
                if (element.Type == type)
                {
                    foreach (var operation in element.Operations)
                    {
                        if (operation.Type.Contains(typeOfDish))
                            return operation;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find average cost
        /// </summary>
        /// <returns></returns>
        public double FindAverageCost()
        {
            double cost = 0;
            int count=0;
            foreach(var proccessing in _processings)
            {
                foreach(var opeartion in proccessing.Operations)
                {
                    cost += opeartion.Cost;
                    count++;
                }
            }
            return cost/count;
        }

        /// <summary>
        /// Find most expensive operations to process
        /// </summary>
        /// <returns></returns>
        public List<IOperation> MostExpensiveOperations() 
        {
            List<IOperation> operations = new List<IOperation> { };
            double averageCost = 0;
            averageCost = FindAverageCost();
            foreach(var processing in _processings)
            {
                foreach(var operation in processing.Operations)
                {
                    if (operation.Cost >= averageCost)
                        operations.Add(operation);
                }
            }
            return operations;
        }

        /// <summary>
        /// Find free production capacity
        /// </summary>
        /// <returns></returns>
        public List<T> FreeProductionCapacity()
        {
            List<T> elements = new List<T> { };
            foreach(var processing in _processings)
            {
                if (processing.NowProcessed < Diner.productiveCapacity[processing])
                {
                    elements.Add(processing);
                }
            }
            return elements;
        }

        /// <summary>
        /// Serialize data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Serialize(string path, ISerialization serialization)
        {
            serialization.Save<T>(_processings, path);
        }

        /// <summary>
        /// Desereliaze data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="serialization"></param>
        public void Deserialize(string path, ISerialization serialization)
        {
            _processings = serialization.Restore<T>(path);
        }
    }
}
