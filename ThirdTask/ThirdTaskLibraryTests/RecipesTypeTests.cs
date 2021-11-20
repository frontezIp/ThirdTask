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
    public class RecipesTypeTests
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
        /// Checks find recipe method by given name and type of dish of recipe
        /// </summary>
        [TestMethod]
        public void FindRecipe_ShouldReturnSalad()
        {
            //Assert
            chief.CreateRecipe("Salad", "Cezar");
            Dictionary<Ingredients, int> ingredients = new Dictionary<Ingredients, int> { [cucumber] = 1, [potato] = 2 };
            chief.AddIngredients(ingredients);
            chief.AddProcessMethod(mix);
            chief.FinishCreating();
            Recipe expected = Diner.recipes[0];
            Recipe actual;

            //Act
            actual = Diner.recipes.Find("Cezar", "Salad");

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
