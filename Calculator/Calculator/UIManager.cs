using System;
using System.Windows;
using System.Diagnostics;

namespace Calculator
{
    public enum LastOperator
    {
       None,
       Division,
       Plus,
       Minus,
       Multiply,
       PlusOnMinus,
    }
    public enum LastFunction
    {
        None,
        Clear,
        ClearAll,
        Delete,
    }
    class UIManager
    {
        private MemoryManager memoryManager;
        private ComputeManager computeManager = new ComputeManager();
        public MainWindow Window { get; set; }

        private double currentValue = 0;
        private double beforeValue = 0;
        private int decimalPointcount = 1;
        private bool isDecimalPoint = false;

        private LastOperator lastOperator = LastOperator.None;
        private LastFunction lastFunction = LastFunction.None;

        private bool isOnOff = false;

        //연산자 클릭?
        private bool isOperatorClicked = true;
        private bool isNumberClicked = true;
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
                //곱하면 자기 자신이 나오는 거
                Trace.WriteLine("/");
                lastOperator = LastOperator.Division;
                isDecimalPoint = false;
                beforeValue = 1;
                Trace.WriteLine($"beforeValue :{beforeValue} +currentValue: {currentValue} ");
                
                currentValue = computeManager.Divide(beforeValue, currentValue);
                Window.SetOutputText(currentValue.ToString());
                currentValue = beforeValue;
            }
            else if (stringValue == "*")
            {
                //항등원
                Trace.WriteLine("*");
                lastOperator = LastOperator.Multiply;
                isDecimalPoint = false;

                if (isOperatorClicked)
                {
                    currentValue = computeManager.Multiply(beforeValue, currentValue);
                    beforeValue = currentValue;
                    Window.SetOutputText(currentValue.ToString());
                    currentValue = 0;
                }
            }
            else if (stringValue == "-")
            {
                
                lastOperator = LastOperator.Minus;
                isDecimalPoint = false;

                if(isOperatorClicked)
                {
                    beforeValue = 0;
                    currentValue = computeManager.Subtract(beforeValue, currentValue);
                    Window.SetOutputText(currentValue.ToString());
                    isOperatorClicked = false;
                    isNumberClicked = false;
                }
                else
                {
                    if (isNumberClicked)
                    {
                        Trace.WriteLine("---");
                        beforeValue = currentValue;
                        currentValue = computeManager.Subtract(beforeValue, currentValue);
                        Trace.WriteLine($"--: {beforeValue}");
                        Window.SetOutputText(beforeValue.ToString());
                        isNumberClicked = false;
                    }
                    else
                    {
                        currentValue = computeManager.Subtract(beforeValue, currentValue);
                        beforeValue = currentValue;
                        Window.SetOutputText(currentValue.ToString());
                        currentValue = 0;
                    }

                }

            }
            else if (stringValue == "+")
            {
                isOperatorClicked = false;
                lastOperator = LastOperator.Plus;
                isDecimalPoint = false;
                currentValue = computeManager.Add(beforeValue, currentValue);
                beforeValue = currentValue;
                Window.SetOutputText(currentValue.ToString());
                currentValue = 0;

            }
            else if(stringValue == "=")
            {
                isDecimalPoint = false;
                double result = 0;

                if (lastOperator == LastOperator.Plus)
                    result = computeManager.Add(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Minus)
                    result = computeManager.Subtract(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Multiply)
                    result = computeManager.Multiply(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Division)
                    result = computeManager.Divide(beforeValue, currentValue);

                Trace.WriteLine($" result: {result}");
                Window.SetOutputText(result.ToString());
                isOperatorClicked = true;
                isNumberClicked = true;
            }
            
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

        public void FunctionButtonClicked(string stringValue)
        {
            if (stringValue == "C")
            {
                lastFunction = LastFunction.Clear;
                isDecimalPoint = false;

                beforeValue = 0;
                currentValue = 0;
                Window.SetOutputText("0");
                isOperatorClicked = true;
                isNumberClicked = true;
                //ComputeManager.Clear
            }
            else if (stringValue == "CE")
            {
                lastFunction = LastFunction.ClearAll;
                isDecimalPoint = false;

                beforeValue = 0;
                currentValue = 0;
                Window.SetOutputText("0");

                isOperatorClicked = true;
                isNumberClicked = true;
                //ComputeManager.ClearAll
            }
            else if (stringValue == "Delete")
            {
                lastFunction = LastFunction.Delete;
                isDecimalPoint = false;
                computeManager.Delete();
            }
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            isOperatorClicked = false;

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
