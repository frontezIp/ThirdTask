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
    public class Dish
    {
        public SequenceMethods<IOperation> ProcessingSequence { get => processingSequence; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }

        private SequenceMethods<IOperation> processingSequence;

        private Dictionary<Ingredients,int> composition;

        string type;

        string name;

        public Dish(SequenceMethods<IOperation> sequence, Dictionary<Ingredients,int> ingredients, string type,string name)
        {
            processingSequence = sequence;
            composition = new Dictionary<Ingredients, int> { };
            TransformIntoComposition(ingredients);
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Transform amount of ingredients into grams
        /// </summary>
        /// <param name="ingredients"></param>
        public void TransformIntoComposition(Dictionary<Ingredients,int> ingredients) 
        {
            foreach(var ingredient in ingredients)
            {
                composition.Add(ingredient.Key, ingredient.Value * ingredient.Key.Weight);
            }
        }
    }
}
