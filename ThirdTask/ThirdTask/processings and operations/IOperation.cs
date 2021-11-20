using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTask
{
    public interface IOperation
    {
        string [] Type { get; }
        
        int AvailableCapacity { get; }

        int Minutes { get; }
        
        double Cost { get; }

        /// <summary>
        /// Process operation in the process order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeOfDish"></param>
        /// <param name="ingredients"></param>
        void Process<T>(T typeOfDish, Dictionary<Ingredients,int> ingredients ) where T:Recipe;
    }
}
