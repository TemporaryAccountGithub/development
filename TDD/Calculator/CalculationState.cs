using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public struct CalculationState<T>
    {
        public List<T> Expressions { get; }
        public List<char> Operations { get; }

        public CalculationState(List<T> expressions, List<char> operations)
        {
            Expressions = expressions;
            Operations = operations;
        }
    }
}
