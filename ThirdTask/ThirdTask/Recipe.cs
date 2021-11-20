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
    public class Recipe
    {
        [DataMember]
        private double costOfTheIngredients;
        [DataMember]
        string nameOfDish;
        [DataMember]
        string typeOfDish;
        [DataMember]
        private double costOfTheProcessings;
        [DataMember]
        private Dictionary<Ingredients,int> requiredIngredients;
        [DataMember]
        private SequenceMethods<IOperation> processingSequence;
        [DataMember]
        public SequenceMethods<IOperation> ProcessingSequence { get => processingSequence; }
        [DataMember]
        public Dictionary<Ingredients,int> RequiredIngredients { get => requiredIngredients; set => requiredIngredients =value; }
        [DataMember]
        public double CostOfTheIngredients { get => costOfTheIngredients; set => costOfTheIngredients = value; }
        [DataMember]
        public double CostOfTheProcessings { get => costOfTheProcessings; set => costOfTheProcessings = value; }
        public string NameOfDish { get => nameOfDish; set => nameOfDish = value; }
        [DataMember]
        public string TypeOfDish { get => typeOfDish; set => typeOfDish = value; }

        public Recipe(string nameOfDish, string typeOfDish)
        {
            requiredIngredients = new Dictionary<Ingredients,int> { };
            processingSequence = new SequenceMethods<IOperation> { };
            NameOfDish = nameOfDish;
            TypeOfDish = typeOfDish;
        }

        /// <summary>
        /// Add get ingredient method to sequence of methods of recipe and calculate cost of given ingredients
        /// </summary>
        /// <param name="ingredients"></param>
        public void AddIngredients(Dictionary<Ingredients,int> ingredients)
        {
            foreach (var paramIngredient in ingredients)
            {
                costOfTheIngredients += paramIngredient.Value * paramIngredient.Key.Cost;
            }
            GetIngredients getIngredients = new GetIngredients();
            getIngredients.AddIngredientToGet(ingredients);
            processingSequence.Add(getIngredients);
        }

        /// <summary>
        /// add process method
        /// </summary>
        /// <param name="operation"></param>
        public void AddMethod(IOperation operation)
        {
            costOfTheProcessings += operation.Cost;
            processingSequence.Add(operation);
            
        }

        /// <summary>
        /// Execute sequence of methods and produce dish by given sequence
        /// </summary>
        public void ExecuteSequence()
        {
            if (processingSequence.ValidateExecute() == true)
            {
                for (int i = 0; i < processingSequence.Count; i++)
                {
                    if (processingSequence[i] is IAddOperation addOperation)
                    {
                        addOperation.Process(this, addOperation.IngredientsToGet);
                    }
                    else
                    {
                        processingSequence[i].Process(this, RequiredIngredients);
                    }
                }
                Diner.expenses.Add(DateTime.Now, (typeOfDish, costOfTheIngredients + costOfTheProcessings));
                Dish dish = new Dish(ProcessingSequence, RequiredIngredients, typeOfDish, nameOfDish);
                requiredIngredients.Clear();
                Diner.dishes.Add(dish);
            }
        }

    }
}
