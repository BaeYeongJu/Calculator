using System;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
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
        private Dictionary<Operator, string> OperatorString = new Dictionary<Operator, string>() {
            { Operator.None,"StrEmpty" },
            { Operator.Division,"/" },
            { Operator.Plus,"+" },
            { Operator.Minus,"-" },
            { Operator.Multiply,"*" },
        };

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
        public string LastOperatorMark = string.Empty;
        private string displayNegate = $"negate({0})";
        private string negate = "negate(0)";

        public UIManager()
        {
            Trace.WriteLine("UIManager");

            inputManager = new InputManager(this);
            outputManager = new OutputManager(this);
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
                    if (IsBeforeValueNumber(Operator.Division, "/"))
                        return;

                    LastOperator = Operator.Division;
                    LastOperatorMark = "/";

                    //+ 입력 > = 입력 > / 입력시 처리
                    if (IsEqualClicked && CurrentValue == 0)
                    {
                        outputManager.DisplayOperatorAndValue(LastOperatorMark, CurrentValue);
                        outputManager.DisplayZeroValue();
                        return;
                    }

                    CalculateOperation(operatorButton, computeManager.Divide(BeforeValue, CurrentValue));
                    break;
                case "*":
                    if (IsBeforeValueNumber(Operator.Multiply, "*"))
                        return;

                    LastOperator = Operator.Multiply;
                    LastOperatorMark = "*";

                    CalculateOperation(operatorButton, computeManager.Multiply(BeforeValue, CurrentValue));
                    break;
                case "-":
                    if (IsBeforeValueNumber(Operator.Minus, "-"))
                        return;

                    LastOperator = Operator.Minus;
                    LastOperatorMark = "-";

                    CalculateOperation(operatorButton, computeManager.Subtract(BeforeValue, CurrentValue));
                    break;
                case "+":
                    LastOperator = Operator.Plus;
                    LastOperatorMark = "+";

                    CalculateOperation(operatorButton, computeManager.Add(BeforeValue, CurrentValue));
                    break;
                case "=":

                    CalculateEqual(operatorButton);
                    break;
                case "C":
                    Clear();
                    break;
                case "+/-":
                    
                    /* 임시..
                    //negate() 문자열
                    1번 클릭 : negate(0)
                    2번 클릭 : negate(negate(0))

                    if(LastOperator == Operator.None && CurrentValue == 0)
                    {
                        //Trace.WriteLine($"Negate :{decimal.Negate(0)}");
                        //Window?.SetResultText("정의되지 않는 결과입니다.");
                        displayNegate += negate;
                        Trace.WriteLine($"+/- :{string.Format(displayNegate)}");
                    }
                  
                    //숫자 입력후
                    CurrentValue *= -1;
                    DisplayInputCurrnetValue();
                    */

                    break;
                case "CE":
                    break;
                case "Delete":
                    break;

            }

            Trace.WriteLine($"isOperatorClicked:{IsOperatorClicked} ,isNumberClicked:{IsNumberClicked}");
        }

        public string GetOperatorString(Operator symbol)
        {
            if (symbol == Operator.None)
                return "StrEmpty";
            return "";
        }

        //숫자를 대입
        public bool IsBeforeValueNumber(Operator operatorSymbol, string opeartorValue)
        {
            if (LastOperator == Operator.None) //숫자를 의미
            { 
                BeforeValue = CurrentValue; //값 대입
                outputManager.DisplayOperatorAndValue(opeartorValue, CurrentValue); //값 출력

                LastOperator = operatorSymbol;
                
                LastOperatorMark = OperatorString[LastOperator];
                CurrentValue = 0;
                IsEqualClicked = false;
                return true;
            }
            return false;
        }

        private void CalculateOperation(string opeartorValue, double calculatedValue)
        {
            //== double 클릭, 기존 값이 0이 아닌 경우 
            if (Window?.ClickedButton == Window?.EqualButton && BeforeValue != 0)
            {
                CurrentValue = BeforeValue;
            }
            else
            {
                CurrentValue = calculatedValue;
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

                outputManager.DisplayOperatorWithResultAndValue(LastOperatorMark, OperatorButton, BeforeValue, CurrentValue);

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
            LastOperatorMark = string.Empty;
            IsfirstZeroClicked = false;
            if (Window?.ClickedButton != null)
                Window.ClickedButton = null;
        }

        //숫자 입력
        public void NumberButtonClicked(int number)
        {
            //처음 숫자 클릭한 경우
            if (!IsOperatorClicked && !IsNumberClicked)
                IsNumberClicked = true;

            //+,= 연산자 둘다 사용시에
            if (LastOperator == Operator.Plus && IsEqualClicked)
                Clear();

            //= 연산자 사용시에
            if (IsEqualClicked)
            {
                CurrentValue = 0;
                IsEqualClicked = false;
            }

            inputManager.NumberButtonClicked(number);
        }

        //현재 입력한 값 출력
        public void DisplayInputCurrnetValue() 
        {
            outputManager.DisplayInputCurrnetValue();
        }

        //결과의 값을 반환
        public double GetResultValue() => BeforeValue;
    }
}
