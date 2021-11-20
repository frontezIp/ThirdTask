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
    public abstract class Ingredients
    {
        [DataMember]
        private (int, int) storageConditions;
        [DataMember]
        private List<IProcessingMethod> possibleProcessings;
        [DataMember]
        private double cost;
        [DataMember]
        private int amount;
        [DataMember]
        private bool isStock;
        [DataMember]
        public bool IsStock { get => isStock; set => isStock = value; }
        [DataMember]
        private int usage;
        [DataMember]
        private int wasted;
        [DataMember]
        private int weight;
        [DataMember]
        public (int, int) StorageConditions { get => storageConditions; set => storageConditions = value; }
        [DataMember]
        public double Cost { get => cost; set => cost = value; }
        [DataMember]
        public int Usage { get => usage; set => usage = value; }
        [DataMember]
        public int Amount { get => amount; set => amount = value; }
        [DataMember]
        public int Wasted { get => wasted; set => wasted = value; }
        [DataMember]
        public int Weight { get => weight; set => weight = value; }
        [DataMember]
        public List<IProcessingMethod> PossibleProcessings { get => possibleProcessings; set => possibleProcessings = value; }

        public Ingredients((int,int) tempreture,double cost, int amount, params IProcessingMethod[] methods)
        {
            IsStock = true;
            Cost = cost;
            Amount = amount;
            possibleProcessings = new List<IProcessingMethod>();
            foreach(IProcessingMethod method in methods)
            {
                PossibleProcessings.Add(method);
            }
        }
        public abstract string Name { get; }
      
    }
}
