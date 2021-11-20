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
    public class SequenceMethods<T> where T: IOperation
    {
        private T[] _sequenceOfOperations;

        public SequenceMethods()
        {
            _sequenceOfOperations = new T[0];
        }

        public T this[int index]
        {
            get
            {
                return _sequenceOfOperations[index];
            }
            set
            {
                _sequenceOfOperations[index] = value;
            }
        }

        public int Count { get { return _sequenceOfOperations.Length; } }

        public void Add(T operation)
        {
            var newSequence = new T[_sequenceOfOperations.Length + 1];

            for(int i = 0; i < _sequenceOfOperations.Length; i++)
            {
                newSequence[i] = _sequenceOfOperations[i];
            }
            newSequence[_sequenceOfOperations.Length] = operation;
            _sequenceOfOperations = newSequence;
        }

        public void Clear()
        {
            Array.Clear(_sequenceOfOperations, 0, Count);
        }
        
        /// <summary>
        /// Validate if the given sequence of methods is possible
        /// </summary>
        /// <returns></returns>
        public bool ValidateExecute()
        {
            bool validator = true;
            int countOfRequiredIngredients=0;
            foreach(IOperation operation in _sequenceOfOperations)
            {
                if( operation is IAddOperation addOperation)
                {
                    foreach(var ingredient in addOperation.IngredientsToGet)
                    {
                        countOfRequiredIngredients += ingredient.Value;
                        if (ingredient.Key.Amount < ingredient.Value)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (operation.AvailableCapacity >= countOfRequiredIngredients)
                        validator = true;
                    else
                    {
                        return false;
                    }
                }
            }
            return validator;
        }
    }
}
