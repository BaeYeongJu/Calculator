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
        Multiply
    }

    public class UIManager
    {
        private HistoryManager historyManager;
        private ComputeManager computeManager = new ComputeManager();
        private InputManager inputManager;

        public MainWindow Window { get; set; }

        public double CurrentValue = 0;
        public double BeforeValue = 0;

        public int DecimalPointCount = 0; //소수인 경우 count

        public Operator lastOperator = Operator.None; //숫자

        public bool IsDecimalPoint = false; //소수 이니?
        public bool IsOperatorClicked = false; //연산자 클릭했니?
        public bool IsNumberClicked = false; //숫자 클릭했니?
        public bool IsEqualClicked = false; //= 클릭했니?
        private bool isFunctionClicked = false; //function 기능 클릭했니?
        public bool IsfirstZeroClicked = false; // ex)0/2

        public string OutputFormat = "0.#################";
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
            if (!IsOperatorClicked && !IsNumberClicked)
                IsOperatorClicked = true;

            if (stringValue != ".")
            {
                IsDecimalPoint = false;
                DecimalPointCount = 0;
            }    

            if (stringValue == ".")
            {
                Trace.WriteLine(". click");
                IsDecimalPoint = true;

                Window?.ResultText(CurrentValue + ".");
            }
            else if (stringValue == "/")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Division, "/", "/"))
                    return;

                lastOperator = Operator.Division;
                lastOperatorMark = "/";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Divide, BeforeValue, CurrentValue));
            }
            else if (stringValue == "*")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Multiply, "*", "*"))
                    return;

                lastOperator = Operator.Multiply;
                lastOperatorMark = "*";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Multiply, BeforeValue, CurrentValue));
            }
            else if (stringValue == "-")
            {
                //isDecimalPoint = false;
                if (LastOperatorNone(Operator.Minus, "-", "-"))
                    return;

                lastOperator = Operator.Minus;
                lastOperatorMark = "-";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Subtract, BeforeValue, CurrentValue));
            }
            else if (stringValue == "+")
            {
                //isDecimalPoint = false;
                lastOperator = Operator.Plus;
                lastOperatorMark = "+";

                OperatorCompute(stringValue, computeManager.ReturnComputeFunc(computeManager.Add, BeforeValue, CurrentValue));
            }
            else if (stringValue == "=")
            {
                IsDecimalPoint = false;

                if (lastOperator == Operator.None)
                {
                    CurrentValueOutput(stringValue, CurrentValue);
                }
                else
                {
                    //= 클릭한 상태 
                    if (CurrentValue == 0 && !IsfirstZeroClicked)
                        CurrentValue = BeforeValue;
                    OutputResult(lastOperatorMark, stringValue, BeforeValue, CurrentValue);

                    if (lastOperator == Operator.Plus)
                        BeforeValue = computeManager.Add(BeforeValue, CurrentValue);
                    else if (lastOperator == Operator.Minus)
                        BeforeValue = computeManager.Subtract(BeforeValue, CurrentValue);
                    else if (lastOperator == Operator.Multiply)
                        BeforeValue = computeManager.Multiply(BeforeValue, CurrentValue);
                    else if (lastOperator == Operator.Division)
                    {
                        if (CurrentValue == 0)
                        {
                            //can't calculate
                            ZeroText();
                            return;
                        }
                        BeforeValue = computeManager.Divide(BeforeValue, CurrentValue);
                    }
                    InputResult();
                }
                IsEqualClicked = true;
            }
            else if (stringValue == "+/-")
            {
                if (CurrentValue == 0)
                    return;
                CurrentValue = CurrentValue * -1;
                Input();
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

            Trace.WriteLine($"isOperatorClicked:{IsOperatorClicked} ,isNumberClicked:{IsNumberClicked}");
        }

        private bool LastOperatorNone(Operator setOperator, string opeartorValue, string setLastOperatorMark)
        {
            if (lastOperator == Operator.None)
            {
                BeforeValue = CurrentValue;
                CurrentValueOutput(opeartorValue, CurrentValue);

                lastOperator = setOperator;
                lastOperatorMark = setLastOperatorMark;
                CurrentValue = 0;
                IsEqualClicked = false;
                return true;
            }
            return false;
        }

        private void OperatorCompute(string opeartorValue, double computeFunc)
        {
            //== double 클릭, 기존 값이 0이 아닌 경우 
            if (Window.ClickedButton == Window.EqualButton && BeforeValue != 0)
            {
                CurrentValue = BeforeValue;
            }
            else
            {
                CurrentValue = computeFunc;
                BeforeValue = CurrentValue;
            }

            IsEqualClicked = false;
            Window?.ResultText(CurrentValue.ToString(OutputFormat));
            CurrentValueOutput(opeartorValue, CurrentValue);
            CurrentValue = 0;
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            inputManager.NumberButtonClicked(number);
        }

        public void Clear()
        {
            IsDecimalPoint = false;
            DecimalPointCount = 0;
            IsOperatorClicked = false;
            IsNumberClicked = false;
            CurrentValue = 0;
            BeforeValue = 0;
            Window?.CalculatedText(string.Empty);
            Window?.ResultText("0");
            lastOperator = Operator.None;
            IsEqualClicked = false;
            Window.ClickedButton = null;
            lastOperatorMark = string.Empty;
            IsfirstZeroClicked = false;
        }
        public void Input()
        {
            Trace.WriteLine($"Output decimalPointCount: {DecimalPointCount}");
            Window?.ResultText(string.Format("{0:N" + (DecimalPointCount) + "}", CurrentValue));
        }

        private void InputResult()
        {
            Trace.WriteLine($"OutputResult decimalPointCount: {DecimalPointCount}");
            Window?.ResultText(BeforeValue.ToString(OutputFormat));
        }

        private void ZeroText()
        {
            Window?.ResultText("0으로 나눌 수 없습니다");
        }

        //입력

        //연산자 + 현재 값
        private void CurrentValueOutput(string operatorValue, double currentValue)
        {
            Trace.WriteLine($"SetText stringValue: {operatorValue}, currentValue.ToString(OutputFormat): {currentValue.ToString(OutputFormat)}");
            Window?.CalculatedText(currentValue.ToString(OutputFormat) + operatorValue);
        }

        //= 인 경우
        private void OutputResult(string lastMark, string stringValue, double beforeValue, double currentValue)
        {
            Window?.CalculatedText(beforeValue.ToString(OutputFormat) + lastMark + currentValue.ToString(OutputFormat) + stringValue);
        }

        public double GetCurrentValue() => CurrentValue;
    }
}
