using System;
using System.Windows;
using System.Diagnostics;

namespace Calculator
{
    public enum Function
    {
       None,
       Division,
       Plus,
       Minus,
       Multiply,
       Clear,
       ClearAll,
       Delete,
       PlusOnMinus,
    }
    class UIManager
    {
        private MemoryManager memoryManager;
        public ComputeManager ComputeManager = new ComputeManager();
        public MainWindow Window { get; set; }

        private double currentValue = 0;
        private double beforeValue = 0;
        private int decimalPointcount = 1;
        private bool isDecimalPoint = false;

        private Function function = Function.None;

        private bool isOnOff = false;

        public UIManager()
        {
            
            Trace.WriteLine("UIManager");
        }

        //사칙연산 버튼을 실행
        public void OperatorButtonClicked(string stringValue) 
        {
            if (stringValue == ".")
            {
                isDecimalPoint = true;
            }
            else if (stringValue == "/")
            {
                function = Function.Division;
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "*")
            {
                function = Function.Multiply;
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "-")
            {
                function = Function.Minus;
                isDecimalPoint = false;
                beforeValue = currentValue;
                currentValue = 0;
            }
            else if (stringValue == "+")
            {
                function = Function.Plus;
                isDecimalPoint = false;
                currentValue = ComputeManager.Add(beforeValue, currentValue);
                beforeValue = currentValue;
                Window.SetOutputText(currentValue.ToString());
                currentValue = 0;

            }
            else if(stringValue == "=")
            {
                isDecimalPoint = false;

                double result = 0;

                if (function == Function.Plus)
                    result = ComputeManager.Add(beforeValue, currentValue);
                else if (function == Function.Minus)
                    result = ComputeManager.Subtract(beforeValue, currentValue);
                else if (function == Function.Multiply)
                    result = ComputeManager.Multiply(beforeValue, currentValue);
                else if (function == Function.Division)
                    result = ComputeManager.Divide(beforeValue, currentValue);

                Trace.WriteLine($" result: {result}");
                if (ComputeManager.isMinus)
                    Window.SetOutputText(result.ToString()+"-");
                else
                    Window.SetOutputText(result.ToString());
                currentValue = 0;
            }
            //else if (stringValue == "C")
            //{
            //    function = Function.Clear;
            //    isDecimalPoint = false;
                
            //    beforeValue = 0;
            //    currentValue = 0;
            //    Window.SetOutputText("0");
            //    //ComputeManager.Clear
            //}
            //else if (stringValue == "CE")
            //{
            //    function = Function.ClearAll;
            //    isDecimalPoint = false;
                
            //    beforeValue = 0;
            //    currentValue = 0;
            //    Window.SetOutputText("0");
            //    //ComputeManager.ClearAll
            //}
            //else if (stringValue == "Delete")
            //{
            //    function = Function.Delete;
            //    isDecimalPoint = false;
            //    ComputeManager.Delete();
            //}
            else if (stringValue == "+/-")
            {
                isOnOff = true;

                if (isOnOff)
                {
                    beforeValue = currentValue * -1;
                    Trace.WriteLine($"+/- beforeValue :{beforeValue}");
                    Window.SetOutputText(beforeValue.ToString());

                    isOnOff = false;
                    currentValue = beforeValue;
                }
                //"-" 기호 처리 필요 (UI 보여줄때)
            }
        }

        //숫자를 출력
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
