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
    public class MixSaladDough: MixMethod,IOperation
    {
        [DataMember]
        private int minutes;
        [DataMember]
        private double cost;
        [DataMember]
        private string[] types;
        [DataMember]
        private int availableCapacity;

        public new string[] Type { get => types; }
        public int Minutes { get => minutes; set => minutes = value; }
        public double Cost { get => cost; set => cost = value; }
        
   
        public int AvailableCapacity { get => availableCapacity; }

        public MixSaladDough(int minutes, double cost,params string[] types )
        {
            this.types = new string[types.Length];
            for (int i = 0; i < types.Length; i++)
                this.types[i] = types[i];
            availableCapacity = base.AvalaibleProcessions();
            this.Minutes = minutes;
            this.Cost = cost;
        }

        public void Process<T>(T typeOfDish,Dictionary<Ingredients,int> ingredients) where T: Recipe
        {
            base.AddProcessing(this, ingredients);
            availableCapacity = base.AvalaibleProcessions();
        }
    }
}
