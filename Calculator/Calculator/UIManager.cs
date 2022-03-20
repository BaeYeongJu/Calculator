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
       Equal,
       Negate,
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
        
        private int decimalPointcount = 1; //소수인 경우 count

        private LastOperator lastOperator = LastOperator.None; //숫자

        private bool isDecimalPoint = false; //소수 이니?
        private bool isOperatorClicked = false; //연산자 클릭했니?
        private bool isNumberClicked = false; //숫자 클릭했니?
        private bool isResultClicked = false; //= 클릭했니?
        private bool isFunctionClicked = false; //function 기능 클릭했니?

        private string decimalChange = "0.################";
        private string lastOperatorMark = string.Empty;

        public UIManager()
        {
            Trace.WriteLine("UIManager");
        }

        //사칙연산 버튼을 실행
        public void OperatorButtonClicked(string stringValue) 
        {
            //처음 사칙연산 클릭한 경우
            if (!isOperatorClicked && !isNumberClicked)
                isOperatorClicked = true;

            if (stringValue == ".")
            {
                Trace.WriteLine($".");
                isDecimalPoint = true;
            }
            else if (stringValue == "/")
            {

            }
            else if (stringValue == "*")
            {

            }
            else if (stringValue == "-")
            {

            }
            else if (stringValue == "+")
            {
                lastOperator = LastOperator.Plus;
                lastOperatorMark = "+";

                //== double 클릭, 기존 값이 0일 경우 
                if(Window.ClickedButton == Window.ResultButton && beforeValue != 0)
                {
                    currentValue = beforeValue;
                }
                else
                {
                    currentValue = computeManager.Add(beforeValue, currentValue);
                    beforeValue = currentValue;
                }

                isResultClicked = false;
                Compute(stringValue, currentValue);
                currentValue = 0;
            }
            else if(stringValue == "=")
            {
                if (lastOperator == LastOperator.Plus)
                {
                    //= 클릭한 상태 
                    if (currentValue == 0)
                        currentValue = beforeValue;

                    ComputeResult(lastOperatorMark, stringValue, beforeValue, currentValue);
                    beforeValue = computeManager.Add(beforeValue, currentValue);
                    OutputResult();
                }
                else if (lastOperator == LastOperator.Minus)
                    currentValue = computeManager.Subtract(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Multiply)
                    currentValue = computeManager.Multiply(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Division)
                    currentValue = computeManager.Divide(beforeValue, currentValue);
                else
                    Compute(stringValue, currentValue);

                isResultClicked = true;
            }
            else if (stringValue == "+/-")
            {
                currentValue = currentValue * -1;
                Output();
            }
            else if (stringValue == "C")
            {
                Clear();
            }
            else if (stringValue == "CE")
            {
                
            }
            else if (stringValue == "Delete")
            {

            }

            Trace.WriteLine($"isOperatorClicked:{isOperatorClicked} ,isNumberClicked:{isNumberClicked}");
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            //처음 숫자 클릭한 경우
            if (!isOperatorClicked && !isNumberClicked)
                isNumberClicked = true;

            //+,= 연산자 둘다 사용시에
            if (lastOperator == LastOperator.Plus && isResultClicked)
                Clear();

            //= 연산자 사용시에
            if(isResultClicked)
            {
                currentValue = 0;
                isResultClicked = false;
            }

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

            if (isDecimalPoint)
                currentValue = double.Parse(string.Format(stringformat, currentValue));

            Output();

            Trace.WriteLine($"num currentValue: {currentValue} , num beforeValue: {beforeValue}");
        }

        private void Clear()
        {
            isDecimalPoint = false;
            decimalPointcount = 1;
            isOperatorClicked = false;
            isNumberClicked = false;
            currentValue = 0;
            beforeValue = 0;
            Window.SetComputeText(string.Empty);
            Window.SetOutputText("0");
            lastOperator = LastOperator.None;
            isResultClicked = false;
            Window.ClickedButton = null;
            lastOperatorMark = string.Empty;
        }

        private void Output()
        {
            Window.SetOutputText(currentValue.ToString(decimalChange));
        }

        private void OutputResult()
        {
            Window.SetOutputText(beforeValue.ToString(decimalChange));
            Trace.WriteLine($" Output beforeValue: {beforeValue}, currentValue: {currentValue}");
        }

        //연산자만 
        private void Compute(string stringValue, double currentValue)
        {
            Window.SetComputeText(stringValue + currentValue.ToString(decimalChange));
        }

        //= 인 경우
        private void ComputeResult(string lastMark, string stringValue, double beforeValue, double currentValue)
        {
            Window.SetComputeText(stringValue + beforeValue.ToString(decimalChange) + lastMark + currentValue.ToString(decimalChange));
        }
    }
}
