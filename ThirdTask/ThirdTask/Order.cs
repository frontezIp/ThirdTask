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
    public class Order
    {
        [DataMember]
        private int _numberOfTheClient;

        [DataMember]
        private DateTime _time;
        [DataMember]
        public int NumberOfTheClient { get => _numberOfTheClient; set => _numberOfTheClient = value; }
        [DataMember]
        public DateTime Time { get => _time; set => _time = value; }
        [DataMember]

        private List<Dish> dishes;
        [DataMember]

        private List<Drink> drinks;

        public Order(int number)
        {
            _numberOfTheClient = number;
            drinks = new List<Drink> { };
            dishes = new List<Dish> { };
        }

        /// <summary>
        /// Add drinks to order
        /// </summary>
        /// <param name="drink"></param>
        public void AddDrink(Drink drink)
        {
            drinks.Add(drink);
        }

        /// <summary>
        /// Add dish to the order
        /// </summary>
        /// <param name="dish"></param>
        public void AddDish(Dish dish)
        {
            dishes.Add(dish);
        }

        /// <summary>
        /// Set time when the order has been made
        /// </summary>
        /// <param name="time"></param>
        public void AddTime(DateTime time)
        {
            Time = time;
        }
       
    }
}
