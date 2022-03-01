using System.Diagnostics;
namespace Calculator
{
    class ComputeManager
    {
        public ComputeManager()
        {
            Trace.WriteLine("ComputeManager");
        }
        public void Percent() { }
        public void Delete() { }
        public void DeleteAll() { }
        public void Clear() { }

        public double Add(double firstValue, double secondValue) { return firstValue + secondValue; }
        public double Subtract(double firstValue, double secondValue) { return firstValue - secondValue; }
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
