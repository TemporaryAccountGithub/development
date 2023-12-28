﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public interface ICalculatorParser
    {
        void ValidateExpression (string expression);
        bool IsBracketsExpression(string expression);
        List<string> ParseBracketsExpression(string expression);
        (List<double>, List<char>) ParseBaseExpression(string expression);
    }
}
