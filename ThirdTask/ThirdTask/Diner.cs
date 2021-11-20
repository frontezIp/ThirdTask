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
    public class Diner
    {
        public static Dictionary<string, (int, int)> TempretureCondtitions;

        public static Dictionary<IProcessingMethod, int> productiveCapacity;

        public static IngredientsType<Ingredients> ingredients;

        public static List<Dish> dishes;

        public static List<Drink> drinks;

        public static RecipesType<Recipe> recipes;

        public static ProcessingType<IProcessingMethod> processings;

        public static OrdersType<Order> orders;

        public static Dictionary<DateTime,(string,double)> expenses;

        public static JSONSerialization jSON;

        public  Diner()
        {
            TempretureCondtitions = new Dictionary<string, (int, int)> { };
            TempretureCondtitions.Add("Cold", (-30, -10));
            TempretureCondtitions.Add("Warm", (0, 30));
            TempretureCondtitions.Add("Normal", (0, 0));
            productiveCapacity = new Dictionary<IProcessingMethod, int> { };
            ingredients = new IngredientsType<Ingredients> { };
            dishes = new List<Dish> { };
            drinks = new List<Drink> { };
            recipes = new RecipesType<Recipe> { };
            processings = new ProcessingType<IProcessingMethod> { };
            orders = new OrdersType<Order> { };
            expenses = new Dictionary<DateTime, (string, double)> { };
            jSON = new JSONSerialization();

        }

        /// <summary>
        /// Sum 2 dictionaries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Dictionary<T,int> SumDict<T> (Dictionary<T, int> first, Dictionary<T, int> second)
        {
            foreach(var element in second)
            {
                if (first.ContainsKey(element.Key))
                {
                    first[element.Key] += element.Value;
                }
                else
                {
                    first.Add(element.Key, element.Value);
                }
            }
            return first;
        }


        /// <summary>
        /// Find expenses for a given period of time on drinks and dishes
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Dictionary<string,double> ExpeneseForAPeriodOfTime(DateTime from, DateTime to)
        {
            Dictionary<string, double> expensesToReturn = new Dictionary<string, double> { };
            foreach(var expense in expenses)
            {
                if(expense.Key >= from && expense.Key <= to)
                {
                    if (expensesToReturn.ContainsKey(expense.Value.Item1))
                    {
                        expensesToReturn[expense.Value.Item1] += expense.Value.Item2;
                    }
                    else
                        expensesToReturn.Add(expense.Value.Item1, expense.Value.Item2);
                }
            }
            return expensesToReturn;
        }

        /// <summary>
        /// Save information in the file
        /// </summary>
        /// <param name="ingredientsPath"></param>
        /// <param name="ordersPath"></param>
        /// <param name="recipesPath"></param>
        /// <param name="processingsPath"></param>
        public void SaveToFile(string ingredientsPath,string ordersPath,string recipesPath, string processingsPath)
        {
            ingredients.Serialize(ingredientsPath, jSON);
            orders.Serialize(ordersPath, jSON);
            recipes.Serialize(recipesPath, jSON);
            processings.Serialize(processingsPath, jSON);
        }

        /// <summary>
        /// Load information from file
        /// </summary>
        /// <param name="ingredientsPath"></param>
        /// <param name="ordersPath"></param>
        /// <param name="recipesPath"></param>
        /// <param name="processingsPath"></param>
        public void LoadFromFile(string ingredientsPath, string ordersPath, string recipesPath, string processingsPath)
        {
            ingredients.Deserialize(ingredientsPath, jSON);
            orders.Deserialize(ordersPath, jSON);
            recipes.Deserialize(recipesPath, jSON);
            processings.Deserialize(processingsPath, jSON);
        }

        public class Chief
        {
            SequenceMethods<IOperation> sequenceMethods;

            Dictionary<Ingredients,int> requiredIngredients;

            Recipe currentRecipe;

            bool createStatus;

            public SequenceMethods<IOperation> SequenceMethods { get => sequenceMethods; set => sequenceMethods = value; }
            public Dictionary<Ingredients, int> RequiredIngredients { get => requiredIngredients; set => requiredIngredients = value; }
            public Recipe CurrentRecipe { get => currentRecipe; set => currentRecipe = value; }

            public Chief()
            {
                SequenceMethods = new SequenceMethods<IOperation> { };
                RequiredIngredients = new Dictionary<Ingredients, int> { };
                createStatus = false;

            }
            /// <summary>
            /// Create recipe with given type and name of Dish or Drink
            /// </summary>
            /// <param name="typeOfDish"></param>
            /// <param name="nameOfDish"></param>
            public void CreateRecipe(string typeOfDish, string nameOfDish)
            {
                if (createStatus == false)
                {
                    createStatus = true;
                    CurrentRecipe = new Recipe(nameOfDish, typeOfDish);
                }
            }

            /// <summary>
            /// Add ingredients to the recipe
            /// </summary>
            /// <param name="ingredientsToAdd"></param>
            public void AddIngredients(Dictionary<Ingredients,int> ingredientsToAdd)
            {
                if(createStatus == true)
                    CurrentRecipe.AddIngredients(ingredientsToAdd);
            }


            /// <summary>
            /// Add process method 
            /// </summary>
            /// <param name="processing"></param>
            public void AddProcessMethod(IProcessingMethod processing)
            {
                if(createStatus==true)
                    CurrentRecipe.AddMethod(processings.findOperation(processing.Type, CurrentRecipe.TypeOfDish));
            }

            /// <summary>
            /// Stop creating current recipe
            /// </summary>
            public void FinishCreating()
            {
                if(createStatus == true)
                {
                    recipes.Add(CurrentRecipe);
                    CurrentRecipe = null;
                    createStatus = false;
                }
            }

            /// <summary>
            /// Delete current recipe
            /// </summary>
            public void CancelRecipe()
            {
                createStatus = false;
                CurrentRecipe = null;
            }
        }

        public class Manager
        {
            private bool _orderStatus;

            private Order _currentOrder;

            public Manager()
            {
                _orderStatus = false;
            }

            /// <summary>
            /// Creates order
            /// </summary>
            /// <param name="numberOfTheClient"></param>
            public void CreateOrder(int numberOfTheClient)
            {
                if(_orderStatus == false)
                {
                    _currentOrder = new Order(numberOfTheClient);
                    _orderStatus = true;
                }
            }

            /// <summary>
            /// AddDish in the order
            /// </summary>
            /// <param name="dish"></param>
            public void AddDish(Dish dish)
            {
                if(_orderStatus == true)
                {
                    if (dishes.Contains(dish))
                        _currentOrder.AddDish(dish);
                }
            }

            /// <summary>
            /// Add Drink in the order
            /// </summary>
            /// <param name="drink"></param>
            public void AddDrink(Drink drink)
            {
                if (_orderStatus == true)
                {
                    if (drinks.Contains(drink))
                        _currentOrder.AddDrink(drink);
                }
            }

            /// <summary>
            /// End current order
            /// </summary>
            public void FinishOrder()
            {
                if(_orderStatus == true)
                {
                    _currentOrder.AddTime(DateTime.Now);
                    orders.Add(_currentOrder);
                }
            }

            /// <summary>
            /// Delete current order
            /// </summary>
            public void CancelOrder()
            {
                if(_orderStatus == true)
                {
                    _currentOrder = null;
                    _orderStatus = false;
                }
            }
        }
    }
}
