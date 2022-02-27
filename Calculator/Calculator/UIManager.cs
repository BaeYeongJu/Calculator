using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    class UIManager
    {
        private double currentValue = 0;
        private int decimalPointcount = 0;
        private bool isDecimalPoint = false;

        public double Input(int number)
        {
            if (!isDecimalPoint)
            {
                currentValue = currentValue * 10 + number;
            }
            else
            {
                currentValue = currentValue + Math.Pow(10,-1*decimalPointcount) * number;
                decimalPointcount++;
            }

            return currentValue;
        }
    }
}
