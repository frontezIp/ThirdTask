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
    class GetIngredients : IAddOperation
    {
        [DataMember]
        public string[] Type { get; }
        [DataMember]
        private Dictionary<Ingredients,int> ingredientsToGet;


        [DataMember]
        public Dictionary<Ingredients,int> IngredientsToGet { get => ingredientsToGet; }
        [DataMember]
        public int Minutes => 0;
        [DataMember]
        public double Cost => 0;

        [DataMember]
        public int AvailableCapacity => 0;

        public GetIngredients()
        {
            ingredientsToGet = new Dictionary<Ingredients, int> { };
            Type = new string[0];
        }

        /// <summary>
        /// Update information about used ingredient and add given ingredients in the required ingredients of the recipe 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeOfDish"></param>
        /// <param name="ingredients"></param>
        public void Process<T>(T typeOfDish, Dictionary<Ingredients,int> ingredients) where T : Recipe
        {
            foreach(KeyValuePair<Ingredients,int> ingredient in ingredients)
            {
                ingredient.Key.Amount -= ingredient.Value;
                ingredient.Key.Wasted += ingredient.Value;
                ingredient.Key.Usage += ingredient.Value;

            }
            typeOfDish.RequiredIngredients=Diner.SumDict<Ingredients>(typeOfDish.RequiredIngredients,ingredients);
        }
        /// <summary>
        /// Add ingredients to operation
        /// </summary>
        /// <param name="ingredients"></param>
        public void AddIngredientToGet(Dictionary<Ingredients, int> ingredients)
        {
            ingredientsToGet = Diner.SumDict<Ingredients>(IngredientsToGet, ingredients);
        }
    }
}
