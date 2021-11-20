using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThirdTask;
using System.Threading.Tasks;

namespace ThirdTaskLibraryTests
{
    [TestClass]
    public class ChiefTests
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
            cucumber = new Cucumbers(Diner.TempretureCondtitions["Cold"],300, 20, mix);
            potato = new Potatoes(Diner.TempretureCondtitions["Warm"],400, 20, mix);
            mixOperation = new MixSaladDough(5, 40, "Salad", "Dough");
            chief = new Diner.Chief();
            manager = new Diner.Manager();
            Diner.ingredients.Add(cucumber);
            Diner.ingredients.Add(potato);
            Diner.processings.Add(mix);
            mix.AddOperation(mixOperation);

        }
        /// <summary>
        /// Checks wheter recipe creating or not
        /// </summary>
        [TestMethod]
        public void CreateRecipe_ShouldCreateRecipe()
        {
            // Arrange
            chief.CreateRecipe("Salad", "Cezar");
            bool expected = true;

            // Act
            bool actual = false;
            if (chief.CurrentRecipe != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks wheter ingredients are adding to recipe or not
        /// </summary>
        [TestMethod]
        public void AddIngredients_ShouldAddIngredients()
        {
            // Arrange
            chief.CreateRecipe("Salad", "Cezar");
            Dictionary<Ingredients, int> dict = new Dictionary<Ingredients, int> { [cucumber] = 10, [potato] = 20 };
            chief.AddIngredients(dict);
            double expected = 11000;

            // Act
            double actual = chief.CurrentRecipe.CostOfTheIngredients;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks wheter method is adding to recipe or not
        /// </summary>
        [TestMethod]
        public void AddProcessMethod_ShouldAddMethod()
        {
            chief.CreateRecipe("Salad", "Cezar");
            chief.AddProcessMethod(mix);
            double expected = 40;

            // Act
            double actual = chief.CurrentRecipe.CostOfTheProcessings;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Check finish creating of recipe method 
        /// </summary>
        [TestMethod]
        public void FinishCreating_ShouldAddFinishedRecipeInRecipes()
        {
            // Arrange
            chief.CreateRecipe("Salad", "Cezar");
            bool expected = true;
            chief.FinishCreating();

            // Act
            bool actual = false;
            if (Diner.recipes[0] != null)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}