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
        private Dictionary<Operator, string> operatorString = new Dictionary<Operator, string>() {
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

        public int DecimalPointCount = 0; //소수인 경우 count

        public Operator LastOperator = Operator.None; //숫자

        public bool IsOperatorClicked = false; //연산자 클릭했니? //연산자
        public bool IsNumberClicked = false; //숫자 클릭했니? //InputManager 에서 관리?
        public bool IsEqualClicked = false; //= 클릭했니? //연산자
        private bool isFunctionClicked = false; //function 기능 클릭했니?

        public string LastOperatorMark = string.Empty;

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
                inputManager.IsDecimalPoint = false;
                DecimalPointCount = 0;
            }
            switch (operatorButton)
            {   
                case "/":
                    if (IsBeforeValueNumber(Operator.Division, "/"))
                        return;

                    LastOperator = Operator.Division;
                    LastOperatorMark = "/";

                    //+ 입력 > = 입력 > / 입력시 처리
                    if (IsEqualClicked && inputManager.CurrentValue == 0)
                    {
                        outputManager.DisplayOperatorAndValue(LastOperatorMark, inputManager.CurrentValue);
                        outputManager.DisplayZeroValue();
                        return;
                    }

                    CalculateOperation(operatorButton, computeManager.Divide(inputManager.BeforeValue, inputManager.CurrentValue));
                    break;
                case "*":
                    if (IsBeforeValueNumber(Operator.Multiply, "*"))
                        return;

                    LastOperator = Operator.Multiply;
                    LastOperatorMark = "*";

                    CalculateOperation(operatorButton, computeManager.Multiply(inputManager.BeforeValue, inputManager.CurrentValue));
                    break;
                case "-":
                    if (IsBeforeValueNumber(Operator.Minus, "-"))
                        return;

                    LastOperator = Operator.Minus;
                    LastOperatorMark = "-";

                    CalculateOperation(operatorButton, computeManager.Subtract(inputManager.BeforeValue, inputManager.CurrentValue));
                    break;
                case "+":
                    LastOperator = Operator.Plus;
                    LastOperatorMark = "+";

                    CalculateOperation(operatorButton, computeManager.Add(inputManager.BeforeValue, inputManager.CurrentValue));
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

                    if(LastOperator == Operator.None && inputManager.CurrentValue == 0)
                    {
                        //Trace.WriteLine($"Negate :{decimal.Negate(0)}");
                        //Window?.SetResultText("정의되지 않는 결과입니다.");
                        displayNegate += negate;
                        Trace.WriteLine($"+/- :{string.Format(displayNegate)}");
                    }
                  
                    //숫자 입력후
                    inputManager.CurrentValue *= -1;
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
                inputManager.BeforeValue = inputManager.CurrentValue; //값 대입
                outputManager.DisplayOperatorAndValue(opeartorValue, inputManager.CurrentValue); //값 출력

                LastOperator = operatorSymbol;
                
                LastOperatorMark = operatorString[LastOperator];
                inputManager.CurrentValue = 0;
                IsEqualClicked = false;
                return true;
            }
            return false;
        }

        private void CalculateOperation(string opeartorValue, double calculatedValue)
        {
            //== double 클릭, 기존 값이 0이 아닌 경우 
            if (Window?.ClickedButton == Window?.EqualButton && inputManager.BeforeValue != 0)
            {
                inputManager.CurrentValue = inputManager.BeforeValue;
            }
            else
            {
                inputManager.CurrentValue = calculatedValue;
                inputManager.BeforeValue = inputManager.CurrentValue;
            }

            IsEqualClicked = false; 
            SetResultText(inputManager.CurrentValue.ToString(outputManager.OutputFormat));
            outputManager.DisplayOperatorAndValue(opeartorValue, inputManager.CurrentValue);
            inputManager.CurrentValue = 0;
        }

        private void CalculateEqual(string operatorButton)
        {
            inputManager.IsDecimalPoint = false;

            if (LastOperator == Operator.None)
            {
                outputManager.DisplayOperatorAndValue(operatorButton, inputManager.CurrentValue);
            }
            else
            {
                //= 클릭한 상태 
                if (inputManager.CurrentValue == 0 && !inputManager.IsfirstZeroClicked)
                    inputManager.CurrentValue = inputManager.BeforeValue;

                outputManager.DisplayOperatorWithResultAndValue(LastOperatorMark, operatorButton, inputManager.BeforeValue, inputManager.CurrentValue);

                switch (LastOperator)
                {
                    case Operator.Division:
                        if (inputManager.CurrentValue == 0)
                        {
                            //can't calculate
                            outputManager.DisplayZeroValue();
                            return;
                        }
                        inputManager.BeforeValue = computeManager.Divide(inputManager.BeforeValue, inputManager.CurrentValue);
                        break;
                    case Operator.Plus:
                        inputManager.BeforeValue = computeManager.Add(inputManager.BeforeValue, inputManager.CurrentValue);
                        break;
                    case Operator.Minus:
                        inputManager.BeforeValue = computeManager.Subtract(inputManager.BeforeValue, inputManager.CurrentValue);
                        break;
                    case Operator.Multiply:
                        inputManager.BeforeValue = computeManager.Multiply(inputManager.BeforeValue, inputManager.CurrentValue);
                        break;
                }

                outputManager.DisplayCurrentCalculatedValue();
            }
            IsEqualClicked = true;
        }

        //각자 초기화 기능을 가지고 있다가 호출.
        public void Clear() //초기화 기능
        {
            inputManager.Clear();
            outputManager.Clear();

            DecimalPointCount = 0;
            IsOperatorClicked = false;
            IsNumberClicked = false;
            LastOperator = Operator.None;
            IsEqualClicked = false;
            LastOperatorMark = string.Empty;
            if (Window?.ClickedButton != null)
                Window.ClickedButton = null;
        }

        //숫자 입력
        public void InputButtonClicked(string value)
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
                inputManager.CurrentValue = 0;
                IsEqualClicked = false;
            }

            inputManager.InputButtonClicked(value);
        }

        //현재 입력한 값 출력
        public void DisplayInputCurrnetValue() 
        {
            outputManager.DisplayInputCurrnetValue();
        }

        //결과의 값을 반환
        public double GetResultValue() => inputManager.BeforeValue;

        public void SetResultText(string data) => Window?.SetResultText(data);
        public void SetCalculatedText(string data) => Window?.SetCalculatedText(data);

        public double CurrentValue() => inputManager.CurrentValue;
    }
}
