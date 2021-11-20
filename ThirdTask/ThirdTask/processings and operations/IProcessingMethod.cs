using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTask
{
 
    public interface IProcessingMethod
    { 
        List<IOperation> Operations { get; }
        int NowProcessed { get; }

        string Type { get; }

        /// <summary>
        /// Add process operation
        /// </summary>
        /// <param name="operation"></param>
        void AddOperation(IOperation operation);

        /// <summary>
        /// Add currently processing ingredients
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="ingredients"></param>
        void AddProcessing(IOperation operation, Dictionary<Ingredients, int> ingredients);
       
    }
}
