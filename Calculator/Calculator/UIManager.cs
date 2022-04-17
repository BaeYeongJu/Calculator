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
        private InputManager inputManager;

        public MainWindow Window { get; set; }
        

        public double currentValue = 0;
        public double beforeValue = 0;

        public int decimalPointCount = 0; //소수인 경우 count

        public Operator lastOperator = Operator.None; //숫자

        public bool isDecimalPoint = false; //소수 이니?
        public bool isOperatorClicked = false; //연산자 클릭했니?
        public bool isNumberClicked = false; //숫자 클릭했니?
        public bool isEqualClicked = false; //= 클릭했니?
        private bool isFunctionClicked = false; //function 기능 클릭했니?
        public bool isfirstZeroClicked = false; // ex)0/2

        public string outputFormat = "0.#################";
        private string lastOperatorMark = string.Empty;

        public UIManager()
        {
            Trace.WriteLine("UIManager");

            inputManager = new InputManager();
            inputManager.uIManager = this;
        }

        //사칙연산 버튼을 실행
        public void OperatorButtonClicked(string stringValue)
        {
            //처음 사칙연산 클릭한 경우
            if (!isOperatorClicked && !isNumberClicked)
                isOperatorClicked = true;

            if (stringValue != ".")
            {
                isDecimalPoint = false;
                decimalPointCount = 0;
            }    

            if (stringValue == ".")
            {
                Trace.WriteLine(". click");
                isDecimalPoint = true;

                Window?.SetOutputText(currentValue + ".");
            }
            else if (stringValue == "/")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Division, "/", "/"))
                    return;

                lastOperator = Operator.Division;
                lastOperatorMark = "/";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Divide, beforeValue, currentValue));
            }
            else if (stringValue == "*")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Multiply, "*", "*"))
                    return;

                lastOperator = Operator.Multiply;
                lastOperatorMark = "*";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Multiply, beforeValue, currentValue));
            }
            else if (stringValue == "-")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Minus, "-", "-"))
                    return;

                lastOperator = Operator.Minus;
                lastOperatorMark = "-";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Subtract, beforeValue, currentValue));
            }
            else if (stringValue == "+")
            {
                //isDecimalPoint = false;
                lastOperator = Operator.Plus;
                lastOperatorMark = "+";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Add, beforeValue, currentValue));
            }
            else if (stringValue == "=")
            {
                isDecimalPoint = false;

                if (lastOperator == Operator.None)
                {
                    SetText(stringValue, currentValue);
                }
                else
                {
                    //= 클릭한 상태 
                    if (currentValue == 0 && !isfirstZeroClicked)
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
                if (currentValue == 0)
                    return;
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

        private bool LastOperatorNone(Operator setOperator, string stringValue, string setLastOperatorMark)
        {
            if (lastOperator == Operator.None)
            {
                beforeValue = currentValue;
                //SetText(stringValue, currentValue);

                lastOperator = setOperator;
                lastOperatorMark = setLastOperatorMark;
                currentValue = 0;
                isEqualClicked = false;
                return true;
            }
            return false;
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
            Window?.SetOutputText(currentValue.ToString(outputFormat));
            //SetText(stringValue, currentValue);
            currentValue = 0;
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            inputManager.NumberButtonClicked(number);
        }

        /*
        public void NumberButtonClicked(int number)
        {

            //처음 숫자 클릭한 경우
            if (!isOperatorClicked && !isNumberClicked)
                isNumberClicked = true;

            //+,= 연산자 둘다 사용시에
            if (lastOperator == Operator.Plus && isEqualClicked)
                Clear();

            if (number == 0)
                isfirstZeroClicked = true;

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
                decimalPointCount++;
                currentValue = currentValue + Math.Pow(10, -1 * decimalPointCount) * number;
            }
            Output();
            Trace.WriteLine($"num currentValue: {currentValue} , num beforeValue: {beforeValue}");
        }
        */

        public void Clear()
        {
            isDecimalPoint = false;
            decimalPointCount = 0;
            isOperatorClicked = false;
            isNumberClicked = false;
            currentValue = 0;
            beforeValue = 0;
            Window?.SetInputText(string.Empty);
            Window?.SetOutputText("0");
            lastOperator = Operator.None;
            isEqualClicked = false;
            Window.ClickedButton = null;
            lastOperatorMark = string.Empty;
            isfirstZeroClicked = false;
        }
        public void Output()
        {
            Trace.WriteLine($"Output decimalPointCount: {decimalPointCount}");
            Window?.SetOutputText(string.Format("{0:N" + (decimalPointCount) + "}", currentValue));
        }

        private void OutputResult()
        {
            Trace.WriteLine($"OutputResult decimalPointCount: {decimalPointCount}");
            Window?.SetOutputText(beforeValue.ToString(outputFormat));
        }

        private void ZeroOutput()
        {
            Window?.SetOutputText("0으로 나눌 수 없습니다");
        }

        //입력

        //연산자만 
        private void SetText(string stringValue, double currentValue)
        {
            Window?.SetInputText(currentValue.ToString(outputFormat) + stringValue);
        }

        //= 인 경우
        private void SetEqualText(string lastMark, string stringValue, double beforeValue, double currentValue)
        {
            Window?.SetInputText(beforeValue.ToString(outputFormat) + lastMark + currentValue.ToString(outputFormat) + stringValue);
        }

        public double GetCurrentValue() => currentValue;
    }
}
