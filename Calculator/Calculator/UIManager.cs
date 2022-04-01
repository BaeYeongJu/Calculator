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
    }

    public class UIManager
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
        private bool isZeroClicked = false;

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
                LastOperatorNone(Operator.Division, stringValue, "/");
                lastOperator = Operator.Division;
                lastOperatorMark = "/";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Divide, beforeValue, currentValue));
            }
            else if (stringValue == "*")
            {
                LastOperatorNone(Operator.Multiply, stringValue, "*");

                lastOperator = Operator.Multiply;
                lastOperatorMark = "*";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Multiply, beforeValue, currentValue));
            }
            else if (stringValue == "-")
            {

                LastOperatorNone(Operator.Minus, stringValue, "-");

                lastOperator = Operator.Minus;
                lastOperatorMark = "-";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Subtract, beforeValue, currentValue));
            }
            else if (stringValue == "+")
            {
                lastOperator = Operator.Plus;
                lastOperatorMark = "+";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Add, beforeValue, currentValue));
            }
            else if (stringValue == "=")
            {

                if (lastOperator == Operator.None)
                {
                    SetText(stringValue, currentValue);
                }
                else 
                {
                    //= 클릭한 상태 
                    if (currentValue == 0 && !isZeroClicked)
                        currentValue = beforeValue;
                    SetEqualText(lastOperatorMark, stringValue, beforeValue, currentValue);

                    if (lastOperator == Operator.Plus)
                        beforeValue = computeManager.Add(beforeValue, currentValue);
                    else if (lastOperator == Operator.Minus)
                        beforeValue = computeManager.Subtract(beforeValue, currentValue);
                    else if (lastOperator == Operator.Multiply)
                        beforeValue = computeManager.Multiply(beforeValue, currentValue);
                    else if (lastOperator == Operator.Division)
                    {
                        if (currentValue == 0)
                        {
                            //can't calculate
                            ZeroOutput();
                            return;
                        }
                        beforeValue = computeManager.Divide(beforeValue, currentValue);
                    }
                    OutputResult();
                }
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

        private void LastOperatorNone(Operator setOperator, string stringValue, string setLastOperatorMark)
        {
            if (lastOperator == Operator.None)
            {
                beforeValue = currentValue;
                SetText(stringValue, currentValue);

                lastOperator = setOperator;
                lastOperatorMark = setLastOperatorMark;
                currentValue = 0;
                isEqualClicked = false;
                return;
            }
        }

        private void OperatorCompute(string stringValue, double computeFunc)
        {
            //== double 클릭, 기존 값이 0이 아닌 경우 
            if (Window.ClickedButton == Window.EqualButton && beforeValue != 0)
            {
                currentValue = beforeValue;
            }
            else
            {
                currentValue = computeFunc;
                beforeValue = currentValue;
            }

            isEqualClicked = false;
            SetText(stringValue, currentValue);
            currentValue = 0;
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

            if (number == 0)
                isZeroClicked = true;

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
                string stringformat = "{0:N" + (decimalPointCount - 1) + "}";
                currentValue = double.Parse(string.Format(stringformat, currentValue));
            }
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
            Window.SetInputText(string.Empty);
            Window.SetOutputText("0");
            lastOperator = Operator.None;
            isEqualClicked = false;
            Window.ClickedButton = null;
            lastOperatorMark = string.Empty;
            isZeroClicked = false;
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

        private void ZeroOutput()
        {
            Window?.SetOutputText("0으로 나눌 수 없습니다");
        }

        //연산자만 
        private void SetText(string stringValue, double currentValue)
        {
            Window?.SetInputText(currentValue.ToString(decimalChange) + stringValue);
        }

        //= 인 경우
        private void SetEqualText(string lastMark, string stringValue, double beforeValue, double currentValue)
        {
            Window?.SetInputText(beforeValue.ToString(decimalChange) + lastMark + currentValue.ToString(decimalChange) + stringValue);
        }

        public double GetCurrentValue() => currentValue;
    }
}
