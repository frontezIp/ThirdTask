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
    public class Drink
    {
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }

        private string type;

        private string name;

        public Drink(string type, string name)
        {
            Type = type;
            Name = name;
        }

    }
}
