using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThirdTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ThirdTaskLibraryTests
{
    [TestClass]
    public class OrdersTypeTests
    {
        Diner diner;
        Potatoes potato;
        Cucumbers cucumber;
        MixMethod mix;
        MixSaladDough mixOperation;
        Diner.Chief chief;
        Diner.Manager manager;

        [TestInitialize]
        public void TestInitialize()
        {
            diner = new Diner();
            mix = new MixMethod();
            cucumber = new Cucumbers(Diner.TempretureCondtitions["Normal"], 300, 20, mix);
            potato = new Potatoes(Diner.TempretureCondtitions["Normal"], 400, 20, mix);
            mixOperation = new MixSaladDough(5, 40, "Salad", "Dough");
            chief = new Diner.Chief();
            manager = new Diner.Manager();
            Diner.ingredients.Add(cucumber);
            Diner.ingredients.Add(potato);
            Diner.processings.Add(mix);
            mix.AddOperation(mixOperation);

        }

        /// <summary>
        /// Checks find orders by given period method
        /// </summary>
        [TestMethod] 
        public void FindByPeriod_ShouldReturnOrder()
        {
            //Assert
            chief.CreateRecipe("Salad", "Cezar");
            Dictionary<Ingredients, int> ingredients = new Dictionary<Ingredients, int> { [cucumber] = 1, [potato] = 2 };
            chief.AddIngredients(ingredients);
            chief.AddProcessMethod(mix);
            chief.FinishCreating();
            Diner.recipes[0].ExecuteSequence();
            manager.CreateOrder(300);
            manager.AddDish(Diner.dishes[0]);
            manager.FinishOrder();
            DateTime from = new DateTime();
            DateTime to = new DateTime(2021, 12, 20);
            Order expected = Diner.orders[0];

            //Act
            List<Order> actual = Diner.orders.FindOrderForGinenPeriod(from, to);

            //Assert
            Assert.AreEqual(expected, actual[0]);
        }
    }
}
