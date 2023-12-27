using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public interface ICalculatorParser
    {
        List<string> ParseExpression(string expression);
    }
}
