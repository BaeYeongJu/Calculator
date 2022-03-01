using System;
using System.Diagnostics;
namespace Calculator
{
    class ComputeManager
    {
        public ComputeManager()
        {
            Trace.WriteLine("ComputeManager");
        }

        public bool isMinus = false;

        public void Percent() { }
        public void Delete() { Trace.WriteLine("delete"); }
        public void Clear(double firstValue, double secondValue) 
        { 
            Trace.WriteLine("Clear"); 
            firstValue = 0; 
            secondValue = 0;
        }
        public void ClearAll(double firstValue, double secondValue) 
        { 
            Trace.WriteLine("ClearAll");
            firstValue = 0;
            secondValue = 0;
        }

        public double Add(double firstValue, double secondValue)
        {
            Trace.WriteLine($"compute : {firstValue + secondValue}");
            return firstValue + secondValue; 
        }
        public double Subtract(double firstValue, double secondValue) 
        {
            Trace.WriteLine($"compute : {firstValue - secondValue}");

            double result = firstValue - secondValue;
            if(result < 0)
            {
                result *=-1;
                isMinus = true;
            }

            return result; 
        }
        public double Multiply(double firstValue, double secondValue) { return firstValue * secondValue; }
        public double Divide(double firstValue, double secondValue) { return firstValue / secondValue; }
        public double Result(double value) { return value; }

        public void DecimalPoint() { }
        public void Mathematics() { }
        public void PlusOnMinus() { }

        public void Reciprocal() { }
        public void SquareRoot() { }
        public void Root() { }
    }
}
