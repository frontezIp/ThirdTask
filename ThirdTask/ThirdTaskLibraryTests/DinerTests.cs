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
    public class DinerTests
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
            cucumber = new Cucumbers(Diner.TempretureCondtitions["Cold"], 300, 20, mix);
            potato = new Potatoes(Diner.TempretureCondtitions["Warm"], 400, 20, mix);
            mixOperation = new MixSaladDough(5, 40, "Salad", "Dough");
            chief = new Diner.Chief();
            manager = new Diner.Manager();
            Diner.ingredients.Add(cucumber);
            Diner.ingredients.Add(potato);
            Diner.processings.Add(mix);
            mix.AddOperation(mixOperation);

        }

        /// <summary>
        /// Checks summary of dictionaries method
        /// </summary>
        [TestMethod]
        public void SumDictTests_ShouldOneDictToAnother()
        {
            // Arrange
            Dictionary<Ingredients, int> ingredients1 = new Dictionary<Ingredients, int> { };
            Dictionary<Ingredients, int> ingredients2 = new Dictionary<Ingredients, int> { };
            Dictionary<Ingredients, int> actual = new Dictionary<Ingredients, int> { };
            ingredients1.Add(cucumber,10);
            ingredients2.Add(potato, 20);
            Dictionary<Ingredients, int> expected = new Dictionary<Ingredients, int> {[cucumber]= 10, [potato ]= 20 };

            // Act
            actual = Diner.SumDict<Ingredients>(ingredients1, ingredients2);

            //Assert
            CollectionAssert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Checks wheater are all expenses are returned from given period of time
        /// </summary>
        [TestMethod]
        public void ExpensesForAPeriodOfTime_ShouldReturnAllExpense()
        {
            //Arrange
            Diner.expenses.Add(DateTime.Now, ("Salad", 600));
            Diner.expenses.Add(DateTime.Now, ("Pepsi", 400));
            Diner.expenses.Add(DateTime.Now, ("Salad", 400));
            Dictionary<string, double> expected = new Dictionary<string, double> { ["Salad"] = 1000, ["Pepsi"] = 400 };
            Dictionary<string, double> actual = new Dictionary<string, double> { };
            DateTime from = new DateTime();
            DateTime to = new DateTime(2021, 12, 20);

            // Act
            actual = diner.ExpeneseForAPeriodOfTime(from, to);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checks wheter information is saved in file
        /// </summary>
        [TestMethod]
        public void SaveToFile_ShouldNotReturnAnError()
        {
            // Assert
            chief.CreateRecipe("Salad", "Cezar");
            Dictionary<Ingredients, int> ingredients = new Dictionary<Ingredients, int> { [cucumber] = 1, [potato] = 1 };
            chief.AddIngredients(ingredients);
            chief.AddProcessMethod(mix);
            chief.FinishCreating();
            Diner.recipes[0].ExecuteSequence();
            manager.CreateOrder(300);
            manager.AddDish(Diner.dishes[0]);
            manager.FinishOrder();

            //Act
            diner.SaveToFile("C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Orders.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Recipes.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Ingredients.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Processings.txt");

            // Assert
            Assert.AreEqual(true, true);
        }

        /// <summary>
        /// Checks whether information was loaded from file without and exception
        /// </summary>
        [TestMethod]
        public void LoadFromFile_ShouldNotReturnAnError()
        {
            // Assert
            chief.CreateRecipe("Salad", "Cezar");
            Dictionary<Ingredients, int> ingredients = new Dictionary<Ingredients, int> { [cucumber] = 1, [potato] = 1 };
            chief.AddIngredients(ingredients);
            chief.AddProcessMethod(mix);
            chief.FinishCreating();
            Diner.recipes[0].ExecuteSequence();
            manager.CreateOrder(300);
            manager.AddDish(Diner.dishes[0]);
            manager.FinishOrder();

            //Act
            diner.LoadFromFile("C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Orders.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Recipes.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Ingredients.txt", "C:\\Users\\User\\source\\repos\\ThirdTask\\ThirdTask\\Processings.txt");

            // Assert
            Assert.AreEqual(true, true);
        }

        
    }
}
