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
    [DataContract(IsReference =true)]
    [KnownType(typeof(Cucumbers))]
    [KnownType(typeof(MixSaladDough))]
    public class MixMethod : IProcessingMethod
    {
        [DataMember]
        private int nowProcessed;
        [DataMember]
        private string type;
        [DataMember]
        private List<IOperation> operations;
        public int NowProcessed { get => nowProcessed; set => nowProcessed = value; }
        [DataMember]
        private Dictionary<IOperation,Dictionary<Ingredients,int>> processings;
        public List<IOperation> Operations { get => operations; }
        public string Type { get => type; set => type = value; }

        public MixMethod()
        {
            processings = new Dictionary<IOperation, Dictionary<Ingredients, int>> { };
            operations = new List<IOperation> { };
            type = "Mix";
            Diner.productiveCapacity.Add(this, 3);
        }

        /// <summary>
        /// Gives number of available Processings
        /// </summary>
        /// <returns></returns>
        public int AvalaibleProcessions()
        {
            return Diner.productiveCapacity[this] - nowProcessed;
        }

       
        public void AddProcessing(IOperation operation, Dictionary<Ingredients,int> ingredients)
        {
            processings.Add(operation, ingredients);
            foreach(var ingredient in ingredients)
            {
                nowProcessed += ingredient.Value;
            }
        }

        public void AddOperation(IOperation operation)
        {
            operations.Add(operation);
        }
    }
}
