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
        private OutputManager outputManager;

        public MainWindow Window { get; set; }

        public double CurrentValue = 0;
        public double BeforeValue = 0;

        public int DecimalPointCount = 0; //소수인 경우 count

        public Operator LastOperator = Operator.None; //숫자

        public bool IsDecimalPoint = false; //소수 이니?
        public bool IsOperatorClicked = false; //연산자 클릭했니?
        public bool IsNumberClicked = false; //숫자 클릭했니?
        public bool IsEqualClicked = false; //= 클릭했니?
        private bool isFunctionClicked = false; //function 기능 클릭했니?
        public bool IsfirstZeroClicked = false; // ex)0/2

        public string OutputFormat = "0.#################";
        public string lastOperatorMark = string.Empty;

        public UIManager()
        {
            Trace.WriteLine("UIManager");

            inputManager = new InputManager();
            inputManager.uIManager = this;

            outputManager = new OutputManager();
            outputManager.uIManager = this;
        }

        //사칙연산 버튼을 실행
        public void OperatorButtonClicked(string operatorButton)
        {
            //처음 사칙연산 클릭한 경우
            if (!IsOperatorClicked && !IsNumberClicked)
                IsOperatorClicked = true;

            if (operatorButton != ".")
            {
                IsDecimalPoint = false;
                DecimalPointCount = 0;
            }

            switch (operatorButton)
            {   
                case ".":
                    IsDecimalPoint = true;

                    Window?.SetResultText(CurrentValue + ".");
                    break;
                case "/":
                    if (IsLastOperatorNone(Operator.Division, "/", "/"))
                        return;

                    LastOperator = Operator.Division;
                    lastOperatorMark = "/";

                    CalculateOperation(operatorButton, computeManager.Divide(BeforeValue, CurrentValue));
                    break;
                case "*":
                    if (IsLastOperatorNone(Operator.Multiply, "*", "*"))
                        return;

                    LastOperator = Operator.Multiply;
                    lastOperatorMark = "*";

                    CalculateOperation(operatorButton, computeManager.Multiply(BeforeValue, CurrentValue));
                    break;
                case "-":
                    if (IsLastOperatorNone(Operator.Minus, "-", "-"))
                        return;

                    LastOperator = Operator.Minus;
                    lastOperatorMark = "-";

                    CalculateOperation(operatorButton, computeManager.Subtract(BeforeValue, CurrentValue));
                    break;
                case "+":
                    LastOperator = Operator.Plus;
                    lastOperatorMark = "+";

                    CalculateOperation(operatorButton, computeManager.Add(BeforeValue, CurrentValue));
                    break;
                case "=":

                    CalculateEqual(operatorButton);
                    break;
                case "C":
                    Clear();
                    break;
                case "+/-":
                    break;
                case "CE":
                    break;
                case "Delete":
                    break;

            }

            Trace.WriteLine($"isOperatorClicked:{IsOperatorClicked} ,isNumberClicked:{IsNumberClicked}");
        }

        public bool IsLastOperatorNone(Operator setOperator, string opeartorValue, string lastOperatorValue)
        {
            if (LastOperator == Operator.None)
            {
                BeforeValue = CurrentValue;
                outputManager.DisplayOperatorAndValue(opeartorValue, CurrentValue);

                LastOperator = setOperator;
                lastOperatorMark = lastOperatorValue;
                CurrentValue = 0;
                IsEqualClicked = false;
                return true;
            }
            return false;
        }

        private void CalculateOperation(string opeartorValue, double calculation)
        {
            //== double 클릭, 기존 값이 0이 아닌 경우 
            if (Window?.ClickedButton == Window?.EqualButton && BeforeValue != 0)
            {
                CurrentValue = BeforeValue;
            }
            else
            {
                CurrentValue = calculation;
                BeforeValue = CurrentValue;
            }

            IsEqualClicked = false;
            Window?.SetResultText(CurrentValue.ToString(OutputFormat));
            outputManager.DisplayOperatorAndValue(opeartorValue, CurrentValue);
            CurrentValue = 0;
        }

        private void CalculateEqual(string OperatorButton)
        {
            IsDecimalPoint = false;

            if (LastOperator == Operator.None)
            {
                outputManager.DisplayOperatorAndValue(OperatorButton, CurrentValue);
            }
            else
            {
                //= 클릭한 상태 
                if (CurrentValue == 0 && !IsfirstZeroClicked)
                    CurrentValue = BeforeValue;

                outputManager.DisplayOperatorWithResultAndValue(lastOperatorMark, OperatorButton, BeforeValue, CurrentValue);

                switch (LastOperator)
                {
                    case Operator.Division:
                        if (CurrentValue == 0)
                        {
                            //can't calculate
                            outputManager.DisplayZeroValue();
                            return;
                        }
                        BeforeValue = computeManager.Divide(BeforeValue, CurrentValue);
                        break;
                    case Operator.Plus:
                        BeforeValue = computeManager.Add(BeforeValue, CurrentValue);
                        break;
                    case Operator.Minus:
                        BeforeValue = computeManager.Subtract(BeforeValue, CurrentValue);
                        break;
                    case Operator.Multiply:
                        BeforeValue = computeManager.Multiply(BeforeValue, CurrentValue);
                        break;
                }

                outputManager.DisplayCurrentCalculatedValue();
            }
            IsEqualClicked = true;
        }

        public void Clear()
        {
            IsDecimalPoint = false;
            DecimalPointCount = 0;
            IsOperatorClicked = false;
            IsNumberClicked = false;
            CurrentValue = 0;
            BeforeValue = 0;
            Window?.SetCalculatedText(string.Empty);
            Window?.SetResultText("0");
            LastOperator = Operator.None;
            IsEqualClicked = false;
            lastOperatorMark = string.Empty;
            IsfirstZeroClicked = false;
            if (Window?.ClickedButton != null)
                Window.ClickedButton = null;
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            inputManager.NumberButtonClicked(number);
        }

        public void DisplayInputCurrnetValue() //현재 입력한 값 출력
        {
            outputManager.DisplayInputCurrnetValue();
        }

        public double GetCurrentValue() => BeforeValue;
    }
}
