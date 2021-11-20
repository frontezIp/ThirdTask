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
    public class Potatoes : Ingredients
    {
        [DataMember]
        public override string Name => "Potato";

        public Potatoes((int, int) tempreture, double cost, int amount, params IProcessingMethod[] methods) : base(tempreture, cost, amount, methods)
        {
        }
    }
}
