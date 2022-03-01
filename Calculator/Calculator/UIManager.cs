using System;
using System.Windows;
using System.Diagnostics;
namespace Calculator
{
    class UIManager
    {
        private MemoryManager memoryManager;
        private ComputeManager computeManager;
        public MainWindow Window { get; set; }

        private double currentValue = 0;
        private double beforeValue = 0;
        private int decimalPointcount = 1;
        private bool isDecimalPoint = false;

        public UIManager()
        {
            Trace.WriteLine("UIManager");
            computeManager = new ComputeManager();
        }

        public void FunctionButtonClicked(string stringValue) 
        {
            if (stringValue == ".")
            {
                isDecimalPoint = true;
            }
            else if (stringValue == "/")
            {
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "*")
            {
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "-")
            {
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "+")
            {
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
                Trace.WriteLine($"+ before: {beforeValue} , current :{currentValue}");
            }
            else if(stringValue == "=")
            {
                isDecimalPoint = false;
                computeManager.Result(beforeValue);
            }
        }

        public void NumberButtonClicked(int number)
        {
            if (!isDecimalPoint)
            {
                currentValue = currentValue * 10 + number;
            }
            else
            {
                currentValue = currentValue + Math.Pow(10, -1 * decimalPointcount) * number;
                decimalPointcount++;
            }

            string stringformat = "{0:N" + (decimalPointcount - 1) + "}";
            Window.SetOutputText(string.Format(stringformat, currentValue));
            
        }
    }
}
