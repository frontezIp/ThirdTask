using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTask
{
    public interface IAddOperation : IOperation
    {
        Dictionary<Ingredients,int> IngredientsToGet { get; }

        /// <summary>
        /// Add ingredients to operation
        /// </summary>
        /// <param name="ingredients"></param>
        void AddIngredientToGet(Dictionary<Ingredients, int> ingredients);
        
    }
}
