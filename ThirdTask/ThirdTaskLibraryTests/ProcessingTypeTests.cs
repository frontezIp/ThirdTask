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
    public class ProcessingTypeTests
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
        /// Checks find operation by given type and name of dish method
        /// </summary>
        [TestMethod]
        public void FindOperation_ShouldReturnMixOperation()
        {
            // Assert
            MixSaladDough expected = mixOperation;

            //Act
            IOperation operation = Diner.processings.findOperation("Mix", "Salad");

            //Assert
            Assert.AreEqual(expected, operation);

        }
        
        /// <summary>
        /// Checks find most expensive operations method
        /// </summary>
        [TestMethod]
        public void MostExpensiveOperations_ShouldReturnMixOperation()
        {
            MixSaladDough expected = mixOperation;
            List<IOperation> actual = new List<IOperation> { };

            //Act
            actual = Diner.processings.MostExpensiveOperations();

            //Assert
            Assert.AreEqual(expected, actual[0]);
        }

        /// <summary>
        /// Checks find all free production capacity methods method
        /// </summary>
        [TestMethod]
        public void FreeProductionCapacity_ShouldReturnMixMethod()
        {
            MixMethod expected = mix;
            List<IProcessingMethod> actual = new List<IProcessingMethod> { };

            //Act
            actual = Diner.processings.FreeProductionCapacity();

            //Assert
            Assert.AreEqual(expected, actual[0]);
        }
    }
}
