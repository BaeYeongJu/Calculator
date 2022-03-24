using System;
using System.Diagnostics;
namespace Calculator
{
    public class ComputeManager
    {
        public ComputeManager() { Trace.WriteLine("ComputeManager"); }
        public void Percent() { }
        public void Delete() { Trace.WriteLine("delete"); }
        public void Clear() { Trace.WriteLine("Clear"); }
        public void ClearAll() { Trace.WriteLine("ClearAll"); }

        public double Add(double firstValue, double secondValue)
        {
            Trace.WriteLine($"Add first: {firstValue} , second: {secondValue}");
            Trace.WriteLine($"Add : {firstValue + secondValue}");
            return firstValue + secondValue;
        }
        public double Subtract(double firstValue, double secondValue)
        {
            Trace.WriteLine($"Subtract : {firstValue - secondValue}");
            return firstValue - secondValue;
        }
        public double Multiply(double firstValue, double secondValue)
        {
            Trace.WriteLine($"Multiply : {firstValue * secondValue}");
            return firstValue * secondValue;
        }
        public double Divide(double firstValue, double secondValue)
        {
            Trace.WriteLine($"Divide : {firstValue / secondValue}");
            return firstValue / secondValue;
        }
        public double Result(double value) { return value; }

        public void DecimalPoint() { }
        public void Mathematics() { }
        public void PlusOnMinus() { }

        public void Reciprocal() { }
        public void SquareRoot() { }
        public void Root() { }
    }
}
