using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ThirdTask
{
    [DataContract]
    public class Cucumbers :Ingredients
    {
        [DataMember]
        public override string Name => "Cucumber";

        public Cucumbers((int,int)tempreture,double cost, int amount, params IProcessingMethod[] methods) : base(tempreture,cost, amount, methods)
        {
        }
    }
}
