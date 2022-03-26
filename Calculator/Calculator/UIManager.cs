using System;
using System.Windows;
using System.Diagnostics;

namespace Calculator
{
    public enum Operator
    {
        None,
        Division,
        Plus,
        Minus,
        Multiply,
        Negate,
    }

    class UIManager
    {
        private HistoryManager historyManager;
        private ComputeManager computeManager = new ComputeManager();
        public MainWindow Window { get; set; }

        private double currentValue = 0;
        private double beforeValue = 0;

        private int decimalPointCount = 1; //소수인 경우 count

        private Operator lastOperator = Operator.None; //숫자

        private bool isDecimalPoint = false; //소수 이니?
        private bool isOperatorClicked = false; //연산자 클릭했니?
        private bool isNumberClicked = false; //숫자 클릭했니?
        private bool isEqualClicked = false; //= 클릭했니?
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
                lastOperator = Operator.Plus;
                lastOperatorMark = "+";

                //== double 클릭, 기존 값이 0이 아닌 경우 
                if (Window.ClickedButton == Window.EqualButton && beforeValue != 0)
                {
                    currentValue = beforeValue;
                }
                else
                {
                    currentValue = computeManager.Add(beforeValue, currentValue);
                    beforeValue = currentValue;
                }

                isEqualClicked = false;
                SetOperatorText(stringValue, currentValue);
                currentValue = 0;
            }
            else if (stringValue == "=")
            {
                if (lastOperator == Operator.Plus)
                {
                    //= 클릭한 상태 
                    if (currentValue == 0)
                        currentValue = beforeValue;

                    SetEqualText(lastOperatorMark, stringValue, beforeValue, currentValue);
                    beforeValue = computeManager.Add(beforeValue, currentValue);
                    OutputResult();
                }
                else if (lastOperator == Operator.Minus)
                    currentValue = computeManager.Subtract(beforeValue, currentValue);
                else if (lastOperator == Operator.Multiply)
                    currentValue = computeManager.Multiply(beforeValue, currentValue);
                else if (lastOperator == Operator.Division)
                    currentValue = computeManager.Divide(beforeValue, currentValue);
                else
                    SetOperatorText(stringValue, currentValue);

                isEqualClicked = true;
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
            if (lastOperator == Operator.Plus && isEqualClicked)
                Clear();

            //= 연산자 사용시에
            if (isEqualClicked)
            {
                currentValue = 0;
                isEqualClicked = false;
            }

            if (!isDecimalPoint)
            {
                currentValue = currentValue * 10 + number;
            }
            else
            {
                currentValue = currentValue + Math.Pow(10, -1 * decimalPointCount) * number;
                decimalPointCount++;
            }

            string stringformat = "{0:N" + (decimalPointCount - 1) + "}";

            if (isDecimalPoint)
                currentValue = double.Parse(string.Format(stringformat, currentValue));

            Output();
            Trace.WriteLine($"num currentValue: {currentValue} , num beforeValue: {beforeValue}");
        }

        private void Clear()
        {
            isDecimalPoint = false;
            decimalPointCount = 1;
            isOperatorClicked = false;
            isNumberClicked = false;
            currentValue = 0;
            beforeValue = 0;
            Window.SetComputeText(string.Empty);
            Window.SetOutputText("0");
            lastOperator = Operator.None;
            isEqualClicked = false;
            Window.ClickedButton = null;
            lastOperatorMark = string.Empty;
        }

        private void Output()
        {
            Window?.SetOutputText(currentValue.ToString(decimalChange));
        }

        private void OutputResult()
        {
            Window?.SetOutputText(beforeValue.ToString(decimalChange));
            Trace.WriteLine($" Output beforeValue: {beforeValue}, currentValue: {currentValue}");
        }

        //연산자만 
        private void SetOperatorText(string stringValue, double currentValue)
        {
            Window?.SetComputeText(currentValue.ToString(decimalChange) + stringValue);
        }

        //= 인 경우
        private void SetEqualText(string lastMark, string stringValue, double beforeValue, double currentValue)
        {
            Window?.SetComputeText(beforeValue.ToString(decimalChange) + lastMark + currentValue.ToString(decimalChange) + stringValue);
        }
    }
}
