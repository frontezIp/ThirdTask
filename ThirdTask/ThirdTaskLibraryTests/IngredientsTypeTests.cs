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
    public class IngredientsTypeTests
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
        /// Checks whehter ingredients finding by method or not
        /// </summary>
        [TestMethod]
        public void FindByName_ShouldReturnCucumber()
        {
            //Arrange
            Cucumbers expected = cucumber;

            //Act
            Ingredients actual =Diner.ingredients.FindByName("Cucumber");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks find by tempreture condition method
        /// </summary>
        [TestMethod]
        public void FindByCondtition_ShouldReturnBoth()
        {
            // Arrange
            List<Ingredients> expected = new List<Ingredients> { };
            expected.Add(cucumber);
            expected.Add(potato);

            // Act
            List<Ingredients> actual = Diner.ingredients.FindByCondition(Diner.TempretureCondtitions["Normal"]);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks Find ingredients that are in stock method
        /// </summary>
        [TestMethod]
        public void FindIngredientsInStock_ShouldReturnBoth()
        {

            // Arrange
            List<Ingredients> expected = new List<Ingredients> { };
            expected.Add(cucumber);
            expected.Add(potato);

            // Act
            List<Ingredients> actual = Diner.ingredients.FindInStock();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks find most usable ingredients method
        /// </summary>
        [TestMethod]
        public void FindMostUsable_ShouldReturnPotato()
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
            Potatoes expected = potato;

            //Act
            List<Ingredients> actual = Diner.ingredients.FindMostUsableIngredients();

            //Assert
            Assert.AreEqual(expected, actual[0]);
        }
    }
}
